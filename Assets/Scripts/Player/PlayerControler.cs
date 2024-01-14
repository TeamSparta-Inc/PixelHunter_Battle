using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerControler : MonoBehaviour
{
    Player player;

    [SerializeField] Transform closestMonsterTransform;

    float moveSpeed = 2;

    public bool Move()
    {
        if (closestMonsterTransform == null) return false;

        var position = transform.position;
        var direction = (closestMonsterTransform.position - position).normalized;

        position += direction * (moveSpeed * Time.deltaTime);
        transform.position = position;

        Debug.Log(position);

        FlipSprite(direction.x);

        return true;
    }

    public void FindClosestMonster()
    {
        Collider2D[] hitColliders = BattleSystem.GetColliderInCircle(transform.position, 10, 1<<12);

        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;
        foreach (var hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = hitCollider.transform;
            }
        }

        closestMonsterTransform = closestPlayer;
    }

    public bool CheckClosestMonster()
    {
        return closestMonsterTransform != null;
    }


    protected void FlipSprite(float directionX)
    {
        if (closestMonsterTransform == null) return;

        var transform = this.transform;
        var scale = transform.localScale;
        var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (directionX)
        {
            case > 0.1f:
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
            case < -0.1f:
                localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                break;
        }
    }

    //protected void FlipSprite()
    //{
    //    if (closestMonsterTransform == null) return;

    //    var scale = transform.localScale;
    //    var direction = (closestMonsterTransform.position - transform.position).normalized;
    //    var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

    //    switch (direction.x)
    //    {
    //        case > 0.1f:
    //            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    //            transform.localScale = localScale;
    //            break;
    //        case < -0.1f:
    //            localScale = new Vector3(localScale.x, localScale.y, localScale.z);
    //            transform.localScale = localScale;
    //            break;
    //    }
    //}
}
