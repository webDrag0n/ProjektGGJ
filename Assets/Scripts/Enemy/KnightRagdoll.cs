using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class KnightEnemy : MonoBehaviour
{
    Animator am;
    Rigidbody2D rb;
    public GameObject[] BodyParts;

    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MakeItRagdoll()
    {
        Debug.Log("2222");
        am.enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < BodyParts.Length; i++)
        {
            BodyParts[i].GetComponent<SpriteSkin>().enabled = false;
            BodyParts[i].GetComponent<Rigidbody2D>().isKinematic = false;
            BodyParts[i].GetComponent<Collider2D>().enabled = true;
        }
        //Object.Destroy(gameObject, 5f);
    }
}
