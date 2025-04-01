using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { DamageBoost, SpeedBoost }
    public PowerUpType type;
    public float boostDuration = 5f;

    [HideInInspector] public PowerUpSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<PlayerBoost>();
            if (player != null)
                player.ApplyPowerUp(type, boostDuration);

            spawner.OnPowerUpCollected();
            Destroy(gameObject);
        }
    }
}
