using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Health playerHealth;

    void Start()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        FindFirstObjectByType<HUD>()?.ShowGameOver();
    }

    void OnDestroy() { playerHealth.OnDeath -= HandleDeath; }
}