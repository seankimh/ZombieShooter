using System.Collections;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    private FPSController controller;
    private Gun gun;

    public float speedMultiplier = 2f;
    public float damageMultiplier = 2f;

    private void Start()
    {
        controller = GetComponent<FPSController>();
        gun = GetComponentInChildren<Gun>();
    }

    public void ApplyPowerUp(PowerUp.PowerUpType type, float duration)
    {
        StartCoroutine(HandleBoost(type, duration));
    }

    private IEnumerator HandleBoost(PowerUp.PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.SpeedBoost:
                controller.walkSpeed *= speedMultiplier;
                controller.runSpeed *= speedMultiplier;
                break;
            case PowerUp.PowerUpType.DamageBoost:
                gun.damage *= damageMultiplier;
                break;
        }

        yield return new WaitForSeconds(duration);

        switch (type)
        {
            case PowerUp.PowerUpType.SpeedBoost:
                controller.walkSpeed /= speedMultiplier;
                controller.runSpeed /= speedMultiplier;
                break;
            case PowerUp.PowerUpType.DamageBoost:
                gun.damage /= damageMultiplier;
                break;
        }
    }
}
