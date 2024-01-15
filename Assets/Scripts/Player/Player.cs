using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    private bool isMeleeAttack = true;
    [SerializeField] GameObject meleeAttackRange;
    [SerializeField] GameObject RangedAttackRange;


    public void ChangeAttack()
    {
        isMeleeAttack = !isMeleeAttack;

        meleeAttackRange.SetActive(isMeleeAttack);
        RangedAttackRange.SetActive(!isMeleeAttack);
    }

    public bool GetAttack()
    {
        return isMeleeAttack;
    }
}
