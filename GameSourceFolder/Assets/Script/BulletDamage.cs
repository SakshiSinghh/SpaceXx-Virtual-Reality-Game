//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description :
 * This script is for the bullet collision. So there are few action that will happen when bullet collides with different game objects
 * 
 */


using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageAmount2 = 5f;
    public GameObject bloodParticlesPrefab;
    public Player player_code;
    public GameManager gameManager;
    private bool isShotHealth = false;
    public AudioManager audioManager;

    private void Start()
    {
        player_code = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.CompareTag("Enemy")) //for enemy in level 1
        {
            // Check if the bullet collided with an enemy
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                audioManager.PlaySFX(audioManager.Pain);
                // Deal damage to the enemy
                enemy.TakeDamage(damageAmount);
                // Spawn blood particles
                if (bloodParticlesPrefab != null)
                {
                    GameObject bloodParticles = Instantiate(bloodParticlesPrefab, col.contacts[0].point, Quaternion.identity);
                    bloodParticles.transform.parent = enemy.transform;
                    Destroy(bloodParticles, 2.0f); // Destroy blood particles after 2 seconds
                }
                Destroy(gameObject);


            }
        }
        if (col.gameObject.CompareTag("Enemy1")) //for enemy in level 2 Note: Same Enemy Different tag and script because second level game logic is different
        {
            // Check if the bullet collided with an enemy
            Enemy1 enemy1 = col.gameObject.GetComponent<Enemy1>();
            if (enemy1 != null)
            {
                audioManager.PlaySFX(audioManager.Pain);
                // Deal damage to the enemy
                enemy1.TakeDamage(damageAmount);
                // Spawn blood particles
                if (bloodParticlesPrefab != null)
                {
                    GameObject bloodParticles = Instantiate(bloodParticlesPrefab, col.contacts[0].point, Quaternion.identity);
                    bloodParticles.transform.parent = enemy1.transform;
                    Destroy(bloodParticles, 2.0f); // Destroy blood particles after 2 seconds
                }
                Destroy(gameObject);


            }
        }
        else if (col.gameObject.CompareTag("Enemy2")) //This is for the Advanced AI
        {
            print("Hit the enemy2");
            Enemy2 enemy2 = col.gameObject.GetComponent<Enemy2>();
            if(enemy2 != null)
            {
                audioManager.PlaySFX(audioManager.Pain);
                enemy2.TakeDamage(damageAmount2);
                if(bloodParticlesPrefab != null)
                {
                    print("come out particle");
                    GameObject bloodParticles = Instantiate(bloodParticlesPrefab, col.contacts[0].point, Quaternion.identity);
                    bloodParticles.transform.parent = enemy2.transform;
                    Destroy(bloodParticles, 2.0f); // Destroy blood particles after 2 seconds
                }
                Destroy(gameObject);
            }
        }
        else if (col.gameObject.CompareTag("ENV")) //This is the environment
        {
            Destroy(gameObject);
        }

        else if (col.gameObject.CompareTag("Health") && !isShotHealth) //The healthpickup
        {
            isShotHealth = true;
            if (gameManager != null)
            {
                gameManager.HealthisShot();
            }
            player_code.HealthMax_amt = 100f;
                player_code.Health_amt = player_code.HealthMax_amt;
            player_code.updateHealthImg();
            player_code.UpdateHealthText();
                print("Pick Health");
                col.gameObject.SetActive(false);


            Destroy(gameObject);
        }
       
    }
}
