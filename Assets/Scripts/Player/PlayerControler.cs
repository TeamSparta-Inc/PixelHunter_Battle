using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerControler : MonoBehaviour
{
    Player player;

    [SerializeField] Transform closestMonsterTransform;


    float moveSpeed = 2;

    float radius = 0.4f;
    [SerializeField] float angle = 0f;
    Vector3 centerPosition;

    [SerializeField] GameObject attackCol;
    float tempSpeed;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        centerPosition = transform.position;
        tempSpeed = player.GetAnimationLength(Strings.ANIMATION_MELEEATTACK);
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
    private float elapsedTime = 0f; // 경과 시간
    public float totalTime; // 반원을 이동하는 데 걸리는 총 시간

    public IEnumerator Attack()
    {
        totalTime = tempSpeed;
        while(true)
        {
            elapsedTime += Time.deltaTime;
            yield return null;

            if (elapsedTime < totalTime)
            {
                float angle = (elapsedTime / totalTime) * 150f;
                float radian = angle * Mathf.Deg2Rad;

                // 새 위치 계산 (x축과 y축을 사용)
                Vector3 newPosition = centerPosition + new Vector3(-Mathf.Sin(radian), Mathf.Cos(radian), 0) * radius;
                attackCol.transform.localPosition = newPosition;
            }
            else
            {
                Debug.Log("도착!");
                // 이동 완료 후 초기 위치로 설정
                attackCol.transform.localPosition = centerPosition - new Vector3(0, radius, 0);
                elapsedTime = 0; // 경과 시간 초기화
                yield break;
            }
            
        }
    }

    public void AttackEvent()
    {
        StartCoroutine(Attack());
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

    public void ResetClosestMonster()
    {
        closestMonsterTransform = null;
    }

    public bool CheckClosestMonsterActive()
    {
        return closestMonsterTransform.gameObject.activeSelf;
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

    private void FlipSprite()
    {
        if (closestMonsterTransform == null) return;

        var scale = transform.localScale;
        var direction = (closestMonsterTransform.position - transform.position).normalized;
        var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (direction.x)
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

    private int CheckFlip()
    {
        if (closestMonsterTransform == null) return 0;

        var scale = transform.localScale;
        var direction = (closestMonsterTransform.position - transform.position).normalized;
        var localScale = new Vector3(Mathf.Abs(scale.x), Mathf.Abs(scale.y), Mathf.Abs(scale.z));

        switch (direction.x)
        {
            case > 0.1f:
                localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                Debug.Log("보자잉 : "+ 1);
                return 1;
            case < -0.1f:
                localScale = new Vector3(localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
                Debug.Log("보자잉 : " + -1);
                return -1;
        }

        return 0;
    }
}
