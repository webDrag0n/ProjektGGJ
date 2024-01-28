using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine;

public class AggresiveCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        try
        {
            other.collider.GetComponent<Bouncy>().EnableBounce();
        }
        catch (NullReferenceException e)
        {}
    }
}
