using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anacember : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject kucukCember;

    public GameObject donenCember;

    public GameObject anaCember_;

    private int atisSayisi = 8;

    void Start()
    {
        oyunYoneticisi = GameObject.FindObjectOfWithTag("oyunYoneticisi");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(kucukCember, transform.position, transform.rotation);
            for (int i = 0; i < transform.childCount; i++)
            {
                // her atis yapildiginda sayilari 1 eksiltiyoruz..
                int sayi = Convert.ToInt32(transform.GetChild(i).GetComponentInChildren<Text>().text);
                sayi--;
                if (sayi > 0)
                {
                    transform.GetChild(i).GetComponentInChildren<Text>().text = sayi.ToString();
                }
                else
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            atisSayisi--;
            if (atisSayisi == 8)
            {
                Debug.Log("Oyun kazandýn");
                oyunYoneticisi.transform.GetComponent<oyunYoneticisi>().oyunKazandi();
            }
        }
    }
}