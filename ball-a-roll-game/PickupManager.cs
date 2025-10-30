using UnityEngine;

// Toplanabilir kapsülleri zemin üstünde rastgele oluþturacak script
public class PickupManager : MonoBehaviour
{
    public GameObject pickup; // kapsül prefabý
    public GameObject ground; // zemin nesnesi
    public int amount; // doðacak kapsül adedi

    private void Start()
    {
        // Zemin boyutlarýný al, Plane nesnesi 10*10 birim olduðundan zeminin scale deðerini 10 ile çarparak boyutuna ulaþtýk.
        float groundSizeX = ground.transform.localScale.x * 10f;
        float groundSizeZ = ground.transform.localScale.z * 10f;

        for (int i = 0; i < amount; i++)
        {
            // Zemin boyunca rastgele X ve Z konumu belirle
            float randomX = Random.Range(-groundSizeX / 2f, groundSizeX / 2f);
            float randomZ = Random.Range(-groundSizeZ / 2f, groundSizeZ / 2f);

            // Kapsülün doðacaðý konumu belirle
            Vector3 location = new Vector3(randomX, 3f, randomZ);
            Instantiate(pickup, location, Quaternion.identity); // Kapsülü belirlenen rastgele konumda oluþtur
        }
    }
}