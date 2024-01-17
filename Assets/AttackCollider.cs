using System.Collections;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    Vector2 direction;
    Transform target;
    Rigidbody2D rb;

    float timePassed;
    const float maxDuration = 2f; // 상수로 선언
    const float speed = 10f; // 상수로 선언

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.TAG_MONSTER))
        {
            if (collision.GetComponent<Monster>().TakeDamage(10))
            {
                Debug.Log("ReSet!");
                PlayerControler.isKilled?.Invoke();
            }

            if (gameObject.tag == "RangedAttack")
            {
                gameObject.SetActive(false);
            }
        }

        if (collision.CompareTag("WallX") || collision.CompareTag("WallY"))
        {
            Debug.Log("나 벽에 맞았다");
            BounceOffWall(collision.CompareTag("WallX"));
        }
    }

    public void SkillRangedAttackEvent(Transform target)
    {
        if (target == null) return;
        this.target = target;
        StartCoroutine(SkillRangedAttack());
    }

    public IEnumerator SkillRangedAttack()
    {
        timePassed = 0; // 초기화 위치 변경
        direction = (target.position - rb.transform.position).normalized;
        rb.transform.position = transform.position;

        gameObject.SetActive(true);

        while (timePassed < maxDuration)
        {
            rb.position += direction * speed * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }

        PlayerManager.instance.ReturnSkillProjectile(gameObject);
    }

    void BounceOffWall(bool isWallX)
    {
        direction.x *= isWallX ? -1 : 1;
        direction.y *= isWallX ? 1 : -1;
    }
}