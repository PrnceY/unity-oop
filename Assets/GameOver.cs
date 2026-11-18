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
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }

    void OnDestroy() { playerHealth.OnDeath -= HandleDeath; }
}