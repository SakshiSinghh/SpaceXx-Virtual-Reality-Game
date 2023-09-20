using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    public enum Enemy_state
    {
        patrol,
        idle,
        chase,
        attack,
        attack_2,
        attack_3,
        death,
    }

    public Enemy_state State;

    public GameObject Target_Obj;
    public float Notice_Range;
    public float Attack_Range;
    public int Attack_Range_2;
    public int Attack_Range_3;
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
    public float attackStoppingDistance = 1.0f;
    public GameObject spellPrefab;
    public GameObject spell2Prefab;
    public float spellSpeed = 15.0f;
    public Transform spawnPoint;
    public Transform SpellBeamPoint;
    public AudioManager audioManager;
    private bool hasBeenKilled = false;

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
            return;
        }
        else
        {
            if (Health_amt <= 0)
            {
                State = Enemy_state.death;
                Health_Img.fillAmount = 0.0f;
                _anim.SetTrigger("isDead");
                agent.isStopped = true;
                Destroy(gameObject, 10f);
                HealthBar_Canvas.SetActive(false);
                return;
            }

            if (Target_Obj == null)
            {
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
                if (Target_Obj)
                {
                    float distanceToTarget = Vector3.Distance(Target_Obj.transform.position, transform.position);

                    if (distanceToTarget < Attack_Range)
                    {
                        transform.LookAt(new Vector3(Target_Obj.transform.position.x, transform.position.y, Target_Obj.transform.position.z));
                        State = Enemy_state.attack;
                        _anim.SetTrigger("Short");
                        agent.isStopped = true;
                        // Check if the attack timer has exceeded the attack delay
                        
                            Attack1(); // Perform the attack

                          


                    }
                    else if (distanceToTarget < Attack_Range_2 && distanceToTarget >= Attack_Range)
                    {
                        transform.LookAt(new Vector3(Target_Obj.transform.position.x, transform.position.y, Target_Obj.transform.position.z));
                        State = Enemy_state.attack_2;
                        print("State = Attack");
                        _anim.SetTrigger("mid");
                        print("Anim settrigger");
                        agent.isStopped = true;
                        print("Agent: " + agent.isStopped);
                        
                           
                            
                                              

                       
                    }
                  
                    else if(distanceToTarget < Attack_Range_3 && distanceToTarget >= Attack_Range_2)
                    {
                        transform.LookAt(new Vector3(Target_Obj.transform.position.x, transform.position.y, Target_Obj.transform.position.z));
                        State = Enemy_state.attack_3;
                        _anim.SetTrigger("long");
                        agent.isStopped = true;
                        
                           
                           

                    }

                    else if (distanceToTarget < Notice_Range)
                    {
                        agent.SetDestination(Target_Obj.transform.position);
                    }

                    else
                    {
                        // Transition to patrol only if neither Attack_Range nor Attack_Range_2 conditions are met
                      
                        State = Enemy_state.patrol;
                    }
                }
                else
                {
                    Target_Obj = null;
                    State = Enemy_state.idle;
                }
            }

            else if (State == Enemy_state.attack)
            {
               
            }

            else if (State == Enemy_state.attack_2)
            {
                
            }

            else if (State == Enemy_state.attack_3)
            {
                
            }




            _anim.SetFloat("Speed", agent.velocity.magnitude);

            Health_Img.fillAmount = Health_amt / HealthMax_amt;

            if (Target_Obj == null)
            {
                HealthBar_Canvas.transform.LookAt(Camera.main.transform.position);
            }
            else
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
            _anim.SetTrigger("isDead");
            agent.isStopped = true;
            Destroy(gameObject, 10f);
            HealthBar_Canvas.SetActive(false);
        }
    }



    public void Attack1()
    {
        if (Target_Obj)
        {
            audioManager.PlaySFX(audioManager.AttackSound);
            if (Vector3.Distance(Target_Obj.transform.position, transform.position) < Attack_Range)
                Target_Obj.GetComponent<Player>().Damage(1f, transform.position);
        }
        else
        {
            State = Enemy_state.patrol;
        }
    }




    public void SpellAttack()
    {
        if (Target_Obj)
        {
           
            if (Vector3.Distance(Target_Obj.transform.position, SpellBeamPoint.position) < Attack_Range_2)
            {
                Vector3 _dir = Target_Obj.transform.position;
                Vector3 _targetDirection = (_dir - SpellBeamPoint.position);

                // Call the spell casting logic directly
                audioManager.PlaySFX(audioManager.Spell);
                CastSpell(_targetDirection);
                print("Spell Cast");
            }
        }
        else
        {
            State = Enemy_state.patrol;
        }
    }

    private void CastSpell(Vector3 spellDirection)
    {
        GameObject spellInstance = Instantiate(spellPrefab, SpellBeamPoint.position, Quaternion.identity);

        Rigidbody spellRigidbody = spellInstance.GetComponent<Rigidbody>(); 

        if (spellRigidbody)
        {
            spellRigidbody.isKinematic = false;
            spellRigidbody.velocity = spellDirection * spellSpeed;
        }

        
        
    }

    public void SpellAttack2()
    {
        if (Target_Obj)
        {

            if (Vector3.Distance(Target_Obj.transform.position, spawnPoint.position) < Attack_Range_3)
            {
                Vector3 _dir = Target_Obj.transform.position;
                Vector3 _targetDirection = (_dir - spawnPoint.position);
                audioManager.PlaySFX(audioManager.Spell);

                // Call the spell casting logic directly
                CastSpell2(_targetDirection);
                print("Spell Cast");
            }
        }
        else
        {
            State = Enemy_state.patrol;
        }
    }

    private void CastSpell2(Vector3 spellDirection)
    {
        GameObject spellInstance = Instantiate(spell2Prefab, spawnPoint.position, Quaternion.identity);

        Rigidbody spellRigidbody = spellInstance.GetComponent<Rigidbody>();

        if (spellRigidbody)
        {
            spellRigidbody.isKinematic = false;
            spellRigidbody.velocity = spellDirection * spellSpeed;
        }

        

    }



    public void AttackFinish1()
    {
        State = Enemy_state.patrol;
        agent.isStopped = false;
    }

    public void AttackFinish2()
    {
        
        State = Enemy_state.patrol;
        agent.isStopped = false;

    }

    public void AttackFinish3()
    {
        print("AttackFinish");
        State = Enemy_state.patrol;
        agent.isStopped = false;

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
}
