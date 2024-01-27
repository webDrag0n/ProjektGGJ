using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(1, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-1, 0));
        }
    }
}
