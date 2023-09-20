//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 *Description:
 *This is the mid range spell attack used by the Boss ai
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_1_damage : MonoBehaviour
{
    //public variables
    public float damageAmount = 3f;
    public float destroyDelay = 0.5f;
    public Player player_code;
    public VignetteController vignetteController;

    private void Start()
    {
        player_code = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        vignetteController = GameObject.FindGameObjectWithTag("Vignette").GetComponent<VignetteController>();
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Check if the bullet collided with an enemy
           
                // Deal damage to the player
                player_code.Damage(damageAmount, transform.position);
                print("Spell Damage received");
            // Get the initial position of the vignette effect
            Vector3 initialVignettePosition = vignetteController.transform.position;

            // Move the vignette effect to the target's position
            vignetteController.transform.position = player_code.transform.position;

            // Start a coroutine to return the vignette effect after 1 second
            StartCoroutine(ReturnVignetteAfterDelay(initialVignettePosition));

            Destroy(gameObject);


            
        }
        else if (col.gameObject.CompareTag("ENV"))
        {
            Destroy(gameObject);
        }

        else if (col.gameObject.CompareTag("Shield"))
        {

            Player player = col.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(0f, transform.position);
                Invoke("DestroySpell", destroyDelay);
            }
           
        }

    }
    private void DestroySpell()
    {
        Destroy(gameObject);
    }
    private IEnumerator ReturnVignetteAfterDelay(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(1f);

        // Return the vignette effect to its initial position
        vignetteController.transform.position = targetPosition;
    }
}
