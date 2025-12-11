using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public GameObject gokyuzu1;
    public GameObject gokyuzu2;

    Rigidbody2D rigidbody1_;
    Rigidbody2D rigidbody2_;

    public float arkaPlanHiz = -1.5f;

    float uzunluk;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject []engeller;

    public float zamanSayac;
    int sayac = 0;

    void Start()
    {
        rigidbody1_ = gokyuzu1.GetComponent<Rigidbody2D>();
        rigidbody2_ = gokyuzu2.GetComponent<Rigidbody2D>();

        rigidbody1_.linearVelocity = new Vector2(arkaPlanHiz, 0);
        rigidbody2_.linearVelocity = new Vector2(arkaPlanHiz, 0);

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];

        for(int i =0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector3(-20,-20),Quaternion.identity);
            Rigidbody2D rigidbodyEngel = engeller[i].AddComponent<Rigidbody2D>();
            rigidbodyEngel.linearVelocity = new Vector2(arkaPlanHiz, 0);
            rigidbodyEngel.gravityScale = 0;
        }
    }

    void Update()
    {
        ArkaPlanHareket();
        EngelHareket();
    }

    void ArkaPlanHareket()
    {
        if(gokyuzu1.transform.position.x <= -uzunluk)
        {
            gokyuzu1.transform.position += new Vector3(uzunluk * 2,0);
        }
        if (gokyuzu2.transform.position.x <= -uzunluk)
        {
            gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0);
        }
    }

    void EngelHareket()
    {
        zamanSayac += Time.deltaTime;
        if(zamanSayac >= 1)
        {
            zamanSayac = 0;
            float Yekseni = Random.Range(0.75f,2f);
            engeller[sayac].transform.position = new Vector3(2.13f, Yekseni);

            sayac++;
            if(sayac == 5)
            {
                sayac = 0;
            }
        }
    }

    public void OyunBitti()
    {
        rigidbody1_.linearVelocity = new Vector2(0, 0);
        rigidbody2_.linearVelocity = new Vector2(0, 0);

        for (int i = 0; i < engeller.Length; i++)
        {
            Rigidbody2D rigidbodyEngel = engeller[i].AddComponent<Rigidbody2D>();
            rigidbodyEngel.linearVelocity = new Vector2(0, 0);
            rigidbodyEngel.gravityScale = 0;
        }
    }
}