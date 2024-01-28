using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform arm;
    public Rigidbody2D arm_rb;
    // Start is called before the first frame update
    void Start()
    {
        arm_rb = arm.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (arm.eulerAngles.z < -100)
        {
            arm_rb.AddTorque(0.15f, ForceMode2D.Impulse);
        }
        else if (arm.eulerAngles.z > -20)
        {
            arm_rb.AddTorque(-0.15f, ForceMode2D.Impulse);
        }
    }
}
