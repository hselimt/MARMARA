using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class kusKontrol : MonoBehaviour
{
    public Sprite[] kusSprites;
    SpriteRenderer spriteRenderer;

    bool ileriHareket = true;
    int kusSayacHareket = 0;
    float kusAnimasyonZaman = 0;

    Rigidbody2D rigidbody_;

    public TextMeshProUGUI sayacText;
    int sayacPuan = 0;

    bool oyunDevamEdiyor = true;

    public GameObject oyunKontrol;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody_ = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol");
    }
    
    void Update()
    {
        KusAnimasyon();
        KusHareket();
    }

    void KusAnimasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;

        if(kusAnimasyonZaman > 0.1f)
        {
            kusAnimasyonZaman = 0;
            if (ileriHareket)
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];
                kusSayacHareket++;
                if (kusSayacHareket == 3)
                {
                    kusSayacHareket--;
                    ileriHareket = false;
                }
            }
            else
            {
                spriteRenderer.sprite = kusSprites[kusSayacHareket];
                kusSayacHareket--;
                if (kusSayacHareket == 0)
                {
                    kusSayacHareket++;
                    ileriHareket = true;
                }
            }
        }
    }

    void KusHareket()
    {
        if (Input.GetMouseButtonDown(0) && oyunDevamEdiyor)
        {
            rigidbody_.linearVelocity = new Vector2(0, 0);
            rigidbody_.AddForce(new Vector2(0, 200));
        }
        if (rigidbody_.linearVelocity.y > 0)
        {
            // yukar� gidiyorsa ac�s�n� artt�r�yoruz..
            transform.eulerAngles = new Vector3(0, 0, 30);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -30);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "puan")
        {
            sayacPuan++;
            sayacText.text = "Puan = " + sayacPuan;
        }

        if (other.gameObject.CompareTag("engel")) // .tag ile de yapilabilir
        {
            oyunDevamEdiyor = false;

            oyunKontrol.GetComponent<oyunKontrol>().OyunBitti();
        }
    }
}
