using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public KnightEnemy[] body;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < body.Length; i++)
        {
            Debug.Log("1111");
            body[i].MakeItRagdoll();
        }
    }

    // Update is called once per frame
}
