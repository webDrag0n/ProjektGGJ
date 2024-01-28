using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float speed = 1;
    Vector2 direction = Vector2.zero;

    const float defaultDirectionTime = 5f;
    float directionTime = defaultDirectionTime;

    const float defaultChangeDirectionTime = 0.2f;
    float changeDirectionTime = 0f;

    const float defaultAttackedTime = 3f;
    float attackedTime = 0f;

    Vector2 playerPos;
    float range = 3f;
    float distance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetRandomDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle") && changeDirectionTime <= 0)
        {
            direction = -direction;
            changeDirectionTime = defaultChangeDirectionTime;
        }
    }

    void SetRandomDirection()
    {
        direction = Utils.GetRandomVector();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetDistance();
        SetAnim();
        if (distance <= range || attackedTime > 0)
        {
            FollowPlayer();
        }
        else
        {
            Walk();
        }

        if (changeDirectionTime > 0)
        {
            changeDirectionTime -= Time.deltaTime;
        }
    }

    void GetDistance()
    {
        if (HammerTest.Instance != null)
        {
            GameObject player = HammerTest.Instance.gameObject;
            playerPos = player.transform.position;
            distance = (playerPos - (Vector2)transform.position).magnitude;
        }
    }

    void SetAnim()
    {
        if (direction.magnitude > 0f)
        {
            anim.SetFloat("InputX", direction.x);
            anim.SetFloat("InputY", direction.y);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void FollowPlayer()
    {
        if (attackedTime > 0)
        {
            attackedTime -= Time.deltaTime;
        }
        direction = (playerPos - (Vector2)transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }

    void Walk()
    {
        if (directionTime > 0)
        {
            directionTime -= Time.deltaTime;
        }
        else
        {
            SetRandomDirection();
            directionTime = defaultDirectionTime;
        }
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }
}
