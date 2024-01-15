using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject hitEffect;
    [SerializeField] MonsterFSM FSM;

    public override bool TakeDamage(float value)
    {
        hitEffect.SetActive(true);
        if (CheckHealth())
        {
            deathEffect.SetActive(true);
            
        }
        return base.TakeDamage(value);
    }
}
