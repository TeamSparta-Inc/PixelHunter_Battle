using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Strings.TAG_MONSTER))
        {
            if (collision.GetComponent<Monster>().TakeDamage(10))
            {
                Debug.Log("ReSet!");
                PlayerControler.isKilled?.Invoke();
            }
        }

        if (collision.CompareTag("WallX"))
        {
            Debug.Log("나 벽에 맞았다");
            WallTingX();
        }
        else if (collision.CompareTag("WallY"))
        {
            Debug.Log("나 벽에 맞았다");
            WallTingY();
        }


        if (gameObject.tag == "RangedAttack" && !collision.CompareTag("WallX") && !collision.CompareTag("WallY")) gameObject.SetActive(false);

    }


    Vector2 direction;
    Transform target;
    Rigidbody2D rb;

    float timePassed = 0f;
    float maxDuration = 2f;

    float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SkillRangedAttackEvent(Transform target)
    {
        if (target == null) return;
        this.target = target;

        StartCoroutine(SkillRangedAttack());
    }

    public IEnumerator SkillRangedAttack()
    {
        direction = (target.position - rb.transform.position).normalized;
        rb.transform.position = transform.position;

        gameObject.SetActive(true);


        while (timePassed < maxDuration)
        {
            rb.position += direction * speed * Time.deltaTime;

            timePassed += Time.deltaTime;
            yield return null;
        }

        timePassed = 0;
        PlayerManager.instance.ReturnSkillProjectile(gameObject);
    }



    void WallTingX()
    {
        direction *= new Vector2(-1,1);
    }
    void WallTingY()
    {
        direction *= new Vector2(1,-1);
    }
    
}
