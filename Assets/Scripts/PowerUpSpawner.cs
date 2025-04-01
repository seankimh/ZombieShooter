using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float respawnTime = 10f;

    private GameObject currentInstance;

    void Start()
    {
        SpawnPowerUp();
    }

    public void SpawnPowerUp()
    {
        currentInstance = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        currentInstance.GetComponent<PowerUp>().spawner = this;
    }

    public void OnPowerUpCollected()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnPowerUp();
    }
}
