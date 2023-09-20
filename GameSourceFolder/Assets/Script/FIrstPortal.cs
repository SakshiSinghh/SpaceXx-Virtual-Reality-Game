//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * The portal that is used in to go to level 2
 * Detects the player collider and sends to level 2
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FIrstPortal : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player_code;

    private void Start()
    {
        player_code = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Player hit portal");
            SceneManager.LoadScene("Level2");
        }
    }
}
