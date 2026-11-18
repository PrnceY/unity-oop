using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private Slider suspicionBar;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;

    private Health playerHealth;
    private WeaponHolder weaponHolder;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        weaponHolder = GameObject.FindWithTag("Player").GetComponentInChildren<WeaponHolder>();

        DetectionManager.Instance.OnSuspicionChanged += UpdateSuspicion;
        ObjectiveManager.Instance.OnObjectiveCompleted += UpdateObjective;
        ObjectiveManager.Instance.OnAllCompleted += ShowWin;

        suspicionBar.minValue = 0;
        suspicionBar.maxValue = 100;
        objectiveText.text = "Objective: Collect intel";
    }

    void Update()
    {
        healthText.text = $"HP: {playerHealth.Current:0}";
        if (weaponHolder != null)
        {
            Weapon w = weaponHolder.CurrentWeapon;
            if (w != null) ammoText.text = $"{w.WeaponName} | {w.CurrentAmmo}";
        }
    }

    void UpdateSuspicion(float val) { suspicionBar.value = val; }

    void UpdateObjective(string id)
    {
        if (id == "collect_intel") objectiveText.text = "Objective: Reach extraction";
    }

        void ShowWin()
    {
        objectiveText.text = "Mission Complete!";
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    void OnDestroy()
    {
        if (DetectionManager.Instance != null)
            DetectionManager.Instance.OnSuspicionChanged -= UpdateSuspicion;
    }
}