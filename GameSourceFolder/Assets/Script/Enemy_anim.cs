using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_anim : MonoBehaviour
{
    public Enemy _code;

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
