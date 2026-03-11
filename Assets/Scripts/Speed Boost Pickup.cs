using UnityEngine;

public class SpeedBoostPickup : MonoBehaviour
{
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement player = other.GetComponent<CharacterMovement>();

            if (player != null)
            {
                player.ActivateSpeedBoost(duration);
            }

            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 30f);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }
}