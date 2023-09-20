//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*Description:
 * the gamemanager logic used in level 2 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager2 : MonoBehaviour
{
    private int enemiesKilled = 0;
    public GameObject orb;
   
    public void EnemyKilled()
    {
        if (enemiesKilled < 3)
        {
            enemiesKilled++;

            if (enemiesKilled >= 3)
            {

                orb.SetActive(true);
               


            }
        }
    }
}
