using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControler : MonoBehaviour
{
    Transform closestMonsterTransform;
    int moveSpeed = 1;

    private void Start()
    {
        closestMonsterTransform = GameObject.FindWithTag("Player").transform;
    }

    public bool Move()
    {
        if (closestMonsterTransform == null) return false;

        var position = transform.position;
        var direction = (closestMonsterTransform.position - position).normalized;

        position += direction * (moveSpeed * Time.deltaTime);
        transform.position = position;

        FlipSprite(direction.x);

        return true;
    }


    private void FlipSprite(float directionX)
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
}
