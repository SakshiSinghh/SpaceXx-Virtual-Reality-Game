//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/*Decription:
 * This is to set animation events in animation of the Enemy1
 * Level 2 dumb AI
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_anim1 : MonoBehaviour
{
    public Enemy1 _code;

    public void Attack()
    {
        _code.Attack();
    }
    public void AttackFinish()
    {
        //this will run when the attack animation play finish
        _code.AttackFinish();

    }
}
