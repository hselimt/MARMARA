using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kucukCemberKod : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody_;
    bool carptiMi = false;

    GameObject oyunYoneticisi;

    public int hiz;

    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        oyunYoneticisi = GameObject.FindGameObjectWithTag("oyunYoneticisi");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!carptiMi)
        {
            rigidbody_.MovePosition(rigidbody_.position + Vector2.up * hiz * Time.deltaTime);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "donenCember")
        {
            carptiMi = true;
            transform.SetParent(other.transform);
        }

        if(other.gameObject.CompareTag("kucukCember"))
        {
            oyunYoneticisi.GetComponent<oyunYoneticisi>().oyunBitti();
        }
    }
}
