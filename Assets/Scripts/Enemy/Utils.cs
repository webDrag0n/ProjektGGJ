using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector2 GetRandomVector()
    {
        //float randomValue = Random.Range(-1f, 1f);
        //if(randomValue > 0)
        //{
        //    return Vector2.right;
        //}
        //else
        //{
        //    return Vector2.left;
        //}
        Vector2 v2 = new Vector2(Random.Range(-10.0f, 10.0f), 0);
        return v2.normalized;
    }
}