//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Fog -> like acid smoke will instantiate after the enemy kills the enemies
 *will deal more damage to the player gradually. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogDamage : MonoBehaviour
{
    public float damageInterval = 3f; // Time interval between damage ticks
    public float damageAmount = 1f; // Amount of damage to inflict

    private Player player; // Reference to the player script

    private void Start()
    {
        // Find the Player script in the scene
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("FogDamage script: Player script not found in the scene!");
        }

        // Start the coroutine to inflict damage over time
        StartCoroutine(InflictDamageOverTime());
    }

    private IEnumerator InflictDamageOverTime()
    {
        while (true)
        {
            if (player != null)
            {
                // Inflict damage to the player
                player.Damage(damageAmount, transform.position);
            }

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
