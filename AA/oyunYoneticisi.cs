using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunYoneticisi : MonoBehaviour
{
    GameObject anaCember;
    GameObject donenCember;

    public Animator animator;

    void Start()
    {
        anaCember = GameObject.FindGameObjectWithTag("anaCember");
        donenCember = GameObject.FindGameObjectWithTag("donenCember");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void oyunBitti()
    {
        Debug.Log("Oyun Bitti");

        donenCember.GetComponent<donenCemberKod>().enabled = false;
        // donme olayýný pasif hale getirdik
        anaCember.GetComponent<anacember>().enabled = false;

        animator.SetTrigger("OYUNBÝTTÝ");
    }
}