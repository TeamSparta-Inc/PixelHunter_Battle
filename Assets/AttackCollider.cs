using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] PlayerControler playerControler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Strings.TAG_MONSTER)) return;

        if (collision.GetComponent<Monster>().TakeDamage(10))
        {
            Debug.Log("ReSet!");
            playerControler.ResetClosestMonster();
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag(Strings.TAG_MONSTER)) return;

    //    playerControler.ResetClosestMonster();

    //}
}
