//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*
 * Description :
 * Game manager script for level 1. It will check whether the 3 enemies are killed.
 * In enemy script a bool used to check the existence of the number of enemies in level 1. 
 * Then the consequent objects will appear
 */



using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public GameObject healthAppear;
    public GameObject DiamondToAppear;
    public GameObject DiamondSocket;

    private int enemiesKilled = 0;

    public void EnemyKilled()
    {
        if (enemiesKilled < 3)
        {
            enemiesKilled++;

            if (enemiesKilled >= 3)
            {
                
                healthAppear.SetActive(true);
               
               
            }
        }
    }

    public void HealthisShot()
    {
        DiamondToAppear.SetActive(true);
        DiamondSocket.SetActive(true);

    }

    
}
