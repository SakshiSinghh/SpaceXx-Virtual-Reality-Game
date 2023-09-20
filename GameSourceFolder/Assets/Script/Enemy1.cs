//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description:
 * This Enemy1 script will be used by the simple Ai in advanced scene
 * reason: different GameManager
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class Enemy1 : MonoBehaviour
{
    public enum Enemy_state
    {
        patrol,
        idle,
        chase,
        attack,
        death
    }

    //public variables
    public Enemy_state State;

    public GameObject Target_Obj;
    public float Notice_Range;
    public float Attack_Range;
    public float Health_amt;
    public Image Health_Img;
    public GameObject HealthBar_Canvas;
    public float HealthMax_amt;
    public Animator _anim;
    public NavMeshAgent agent;
    [Range(0, 100)] public float speed;
    [Range(1, 500)] public float walkradius;
    public LayerMask Wall_Mask;
    public GameManager2 gameManager2;

    
    //--this one only--// private
    private bool hasBeenKilled = false;
    public VignetteController vignetteController;
    public AudioManager audioManager;



    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        if (agent != null)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


    }

    public void Update()
    {
       
        if (State == Enemy_state.death)
        {
            // Handle death state if needed
            return;
        }
        else
        {
            if (Health_amt <= 0)
            {
                State = Enemy_state.death;
                Health_Img.fillAmount = 0.0f;
                _anim.SetTrigger("death");
                agent.isStopped = true;
                Destroy(gameObject, 10f);
                HealthBar_Canvas.SetActive(false);
                HealthBar_Canvas = null;
                return;
            }

            // Check if the target object (player) is null
            if (Target_Obj == null)
            {
                // Handle the case when the player is killed or does not exist
                // For example, you can go back to the idle state or reset the target.
                agent.isStopped = true;
                State = Enemy_state.idle;
                return;
            }

            if (State == Enemy_state.patrol)
            {
                if (Vector3.Distance(Target_Obj.transform.position, transform.position) < Notice_Range)
                {
                    if (!Physics.Linecast(Target_Obj.transform.position, transform.position, Wall_Mask))
                    {
                        State = Enemy_state.chase;
                    }
                    else if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
                    {
                        agent.SetDestination(RandomNavMeshLocation());
                    }
                }
                else if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(RandomNavMeshLocation());
                }
            }
            else if (State == Enemy_state.chase)
            {
                print(" Chase Now: " + agent.isStopped);
                print("Chase");
                if (Target_Obj) //check target object exists first
                {
                    if (Vector3.Distance(Target_Obj.transform.position, transform.position) < Attack_Range)
                    {
                        // Perform attack
                        State = Enemy_state.attack;
                        
                        agent.isStopped = true;
                        print("Attack :" + agent.isStopped);
                        _anim.SetTrigger("Attack");
                       


                    }
                    else if (Vector3.Distance(Target_Obj.transform.position, transform.position) < Notice_Range)
                    {
                        print("Chase in notice range, chase state " + agent.isStopped);
                        agent.SetDestination(Target_Obj.transform.position);
                    }

                    else
                    {
                        State = Enemy_state.patrol;
                    }
                }
                else //target object died, go back to idle
                {
                    Target_Obj = null;
                    print("Target obj died");
                    State = Enemy_state.patrol;
                }
            }
            else if (State == Enemy_state.attack)
            {

                
            }

           
            _anim.SetFloat("Speed", agent.velocity.magnitude);

            //calculating the fillAmount 
            Health_Img.fillAmount = Health_amt / HealthMax_amt;
            //Health bar to look at the camera always
            if (Target_Obj == null)
            {
                HealthBar_Canvas.transform.LookAt(Camera.main.transform.position);
            }


        }
    }

    public void TakeDamage(float damageAmount)
    {
        Health_amt -= damageAmount;

        if (Health_amt <= 0.0f && !hasBeenKilled)
        {
            hasBeenKilled = true;
            gameManager2.EnemyKilled();
            State = Enemy_state.death;
            Health_Img.fillAmount = 0.0f;
            _anim.SetTrigger("death");
            agent.isStopped = true;
            Destroy(gameObject, 10f);
            HealthBar_Canvas.SetActive(false);
            HealthBar_Canvas = null;

        }
    }

    public void Attack()
    {
        if (Target_Obj)
        {
            audioManager.PlaySFX(audioManager.AttackSound);
            if (Vector3.Distance(Target_Obj.transform.position, transform.position) < Attack_Range)
                Target_Obj.GetComponent<Player>().Damage(2f, transform.position);
            // Get the initial position of the vignette effect
            Vector3 initialVignettePosition = vignetteController.transform.position;

            // Move the vignette effect to the target's position
            vignetteController.transform.position = Target_Obj.transform.position;

            // Start a coroutine to return the vignette effect after 1 second
            StartCoroutine(ReturnVignetteAfterDelay(initialVignettePosition));

        }
      
    }



    public void AttackFinish()
    {
        State = Enemy_state.chase;
        print("Attack Finish");
        agent.isStopped = false;
        print("After Attack Finish "+agent.isStopped);
        print("Go back to patrol");
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkradius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkradius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private IEnumerator ReturnVignetteAfterDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(1f);

        // Return the vignette effect to its initial position
        vignetteController.transform.position = targetPosition;
    }
    
}
