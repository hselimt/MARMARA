using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f; // hareket h�z�
    Rigidbody rb; // fizik componenti

    ScoreManager scoreManager; // skor y�neticisi

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // scriptin ba�l� oldu�u nesne �zerindeki componenti bul
        scoreManager = FindFirstObjectByType<ScoreManager>(); // sahnedeki skor y�neticisini bul
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // Yatay hareket girdisi
        float moveZ = Input.GetAxis("Vertical"); // Dikey hareket girdisi

        Vector3 movement = new Vector3(moveX, 0f, moveZ); // Oyuncunun gitmek istedi�i y�n� belirle
        rb.AddForce(movement * moveSpeed); // Rigidbody'ye gidilmek istenen y�nde kuvvet uygula b�ylece hareketi sa�lam�� oluruz
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Oyuncu kaps�l ile temas etti�inde kaps�l� yok et
            Destroy(other.gameObject);
            scoreManager.CollectPickup();
        }
    }
}