using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float speed = 1;
    public Vector2 direction = Vector2.zero;

    const float defaultDirectionTime = 2f;
    float directionTime = defaultDirectionTime;

    const float defaultChangeDirectionTime = 0.2f;
    float changeDirectionTime = 0f;

    public float attackedTime = 0f;

    Vector2 playerPos;
    public float range = 3f;
    float distance = 0f;

    public LayerMask groundLayer; // 地面的图层
    public float rayDistance = 0.1f; // 射线的检测距离
    public bool isGrounded; // 物体是否在地面上
    private CapsuleCollider2D cc; // 物体的碰撞器组件

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>(); // 获取碰撞器组件
        anim = GetComponent<Animator>();
        SetRandomDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
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
        direction = new Vector2(direction.x, 0);
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
        // 从物体的底部中心发射一条向下的射线，检测是否碰到地面图层
        //Vector2 origin = transform.position + Vector3.down * cc.size.y / 2;
        Vector3 offset = new Vector3(0, 0.162f, 0);
        Vector2 origin = transform.position + offset;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayDistance, groundLayer);
        Debug.DrawRay(origin, Vector2.up * rayDistance, Color.red);

        // 如果碰到地面，设置isGrounded为true，并将刚体的重力缩放设置为0，防止物体下沉
        if (hit.collider != null)
        {
            isGrounded = true;
            rb.gravityScale = 0;
        }
        else
        {
            // 如果没有碰到地面，设置isGrounded为false，并将刚体的重力缩放设置为1，让物体受重力影响
            isGrounded = false;
            rb.gravityScale = 1;
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (!isGrounded)
    //    {
    //        if (collision.transform.tag == "Ground")
    //        {
    //            isGrounded = true;
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Ground")
    //    {
    //        isGrounded = false;
    //    }
    //}
}
