using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_anim : MonoBehaviour
{
    public Enemy2 _code;

    public void Attack1()
    {
        _code.Attack1();
    }
    

    public void SpellAttack()
    {
        _code.SpellAttack();
    }

    public void SpellAttack2()
    {
        _code.SpellAttack2();
    }
    public void AttackFinish1()
    {
        //this will run when the attack animation play finish
        _code.AttackFinish1();

    }

    public void AttackFinish2()
    {
        _code.AttackFinish2();
    }

    public void AttackFinish3()
    {
        _code.AttackFinish3();
    }
}
