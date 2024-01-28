using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float speed = 1;
    public Vector2 direction = Vector2.zero;

    const float defaultDirectionTime = 5f;
    float directionTime = defaultDirectionTime;

    const float defaultChangeDirectionTime = 0.2f;
    float changeDirectionTime = 0f;

    public float attackedTime = 0f;

    Vector2 playerPos;
    public float range = 3f;
    float distance = 0f;

    public LayerMask groundLayer; // �����ͼ��
    public float rayDistance = 0.001f; // ���ߵļ�����
    public bool isGrounded; // �����Ƿ��ڵ�����
    private CapsuleCollider2D cc; // �������ײ�����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>(); // ��ȡ��ײ�����
        anim = GetComponent<Animator>();
        SetRandomDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Obstacle") && changeDirectionTime <= 0)
        {
            direction = -direction;
            SetAnimationDirection();
            //Vector3 scaleChange = new Vector3(-2 * transform.localScale.x, 0, 0);
            //Vector3 newScale = transform.localScale + scaleChange;
            //this.transform.localScale = newScale;
            changeDirectionTime = defaultChangeDirectionTime;
        }
    }

    void SetAnimationDirection()
    {
        if (direction.x * transform.localScale.x <= 0)
        {
            Vector3 scaleChange = new Vector3(-2 * transform.localScale.x, 0, 0);
            Vector3 newScale = transform.localScale + scaleChange;
            this.transform.localScale = newScale;
        }
    }

    void SetRandomDirection()
    {
        direction = Utils.GetRandomVector();
        SetAnimationDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetDistance();
        CheckGround();
        SetAnim();
        if (isGrounded)
        {
            if (distance <= range || attackedTime > 0)
            {
                FollowPlayer();
            }
            else
            {
                Walk();
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
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
        SetAnimationDirection();
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
    void CheckGround()
    {
        // ������ĵײ����ķ���һ�����µ����ߣ�����Ƿ���������ͼ��
        //Vector2 origin = transform.position + Vector3.down * cc.size.y / 2;
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.down * rayDistance, Color.red);

        // ����������棬����isGroundedΪtrue�����������������������Ϊ0����ֹ�����³�
        if (hit.collider != null)
        {
            isGrounded = true;
            rb.gravityScale = 0;
        }
        else
        {
            // ���û���������棬����isGroundedΪfalse�����������������������Ϊ1��������������Ӱ��
            isGrounded = false;
            rb.gravityScale = 1;
        }
    }
}
