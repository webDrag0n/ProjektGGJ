using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    private bool facingRight;
    private Vector3 scale;
    public GameObject character;
    
    void Flip(bool isRight) {
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        if (isRight!=facingRight)
        {
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    private void Start()
    {
        scale = transform.localScale;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;



        Vector3 hingePosition = transform.position;
        Vector3 characterDirection = character.transform.up;
        if (Vector3.Angle(HammerTest.hingeDirection, characterDirection) < 5 || Vector3.Angle(HammerTest.hingeDirection, characterDirection)>175)
        {
            transform.GetComponent<HingeJoint2D>().enabled = false;
            Flip((mousePosition.x - hingePosition.x)<0);
            transform.GetComponent<HingeJoint2D>().enabled = true;
        }

        Debug.Log(Vector3.Angle(HammerTest.hingeDirection, characterDirection));
        
    }
    
}
