//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description :
 * This script is made for the explosiove particle system used in this Game. When the player does the socket interactor with cube in lavel 1 it will trigger the explosion.
 * Plus the  subsequent action that will be made by the player to exit lavel 1
 * smoke and portal 2 is used in level 2
 * to save the script space this script is used to store the actions
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public GameObject bombBlastParticle;
    public GameObject portalPrefab;
    public GameObject Acid_fog;
    public GameObject portal2Prefab;

    private bool hasExploded = false;
    public AudioManager audioManager;

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public void Initialize()
    {
        audioManager.PlaySFX(audioManager.Blast);
        // Instantiate the bomb blast particle system when the Initialize method is called
        Instantiate(bombBlastParticle, transform.position, transform.rotation);
    }

    public void TriggerExplosion()
    {
        if (!hasExploded)
        {
            // Hide the bomb GameObject
            gameObject.SetActive(false);
            audioManager.PlaySFX(audioManager.Portal);
            // Instantiate and show the portal
            Instantiate(portalPrefab, transform.position, transform.rotation);

            hasExploded = true;
        }
    }

    public void smoke()
    {
        Instantiate(Acid_fog, transform.position, transform.rotation);
    }

    public void portal2()
    {
        audioManager.PlaySFX(audioManager.Portal);
        Instantiate(portal2Prefab, transform.position, transform.rotation);
    }

    
}
