using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class donenCemberKod : MonoBehaviour
{
    public int hiz;

    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, hiz * Time.deltaTime);
    }
}
