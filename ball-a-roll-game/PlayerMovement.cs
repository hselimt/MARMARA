using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f; // hareket hýzý
    Rigidbody rb; // fizik componenti

    ScoreManager scoreManager; // skor yöneticisi

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // scriptin baðlý olduðu nesne üzerindeki componenti bul
        scoreManager = FindFirstObjectByType<ScoreManager>(); // sahnedeki skor yöneticisini bul
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal"); // Yatay hareket girdisi
        float moveZ = Input.GetAxis("Vertical"); // Dikey hareket girdisi

        Vector3 movement = new Vector3(moveX, 0f, moveZ); // Oyuncunun gitmek istediði yönü belirle
        rb.AddForce(movement * moveSpeed); // Rigidbody'ye gidilmek istenen yönde kuvvet uygula böylece hareketi saðlamýþ oluruz
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Oyuncu kapsül ile temas ettiðinde kapsülü yok et
            Destroy(other.gameObject);
            scoreManager.CollectPickup();
        }
    }
}