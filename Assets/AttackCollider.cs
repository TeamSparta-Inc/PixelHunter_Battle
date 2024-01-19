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

    [SerializeField] int damage = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽에 부딫혔다!");
            ContactPoint2D contact = collision.GetContact(0);
            BounceOffWall(contact.normal);
        }

        if (gameObject.tag == "RangedAttack" && !collision.gameObject.CompareTag("Wall"))
            gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Strings.TAG_MONSTER))
        {
            if (collision.gameObject.GetComponent<Monster>().TakeDamage(damage))    
            {
                Debug.Log("ReSet!");
                PlayerControler.isKilled?.Invoke();
            }
            if (gameObject.tag == "RangedAttack")
                gameObject.SetActive(false);
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

    void BounceOffWall(Vector2 normal)
    {
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        {
            // X축에 더 큰 충격이 있었음, Y축 방향 유지
            direction = new Vector2(-direction.x, direction.y);
        }
        else
        {
            // Y축에 더 큰 충격이 있었음, X축 방향 유지
            direction = new Vector2(direction.x, -direction.y);
        }
    }


    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
} 