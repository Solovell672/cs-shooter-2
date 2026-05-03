using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private Image healthBar;
    
    private WeaponManager weaponManager;
    private PlayerHealth playerHealth;
    private WaveManager waveManager;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    private void Update()
    {
        UpdateAmmoDisplay();
        UpdateHealthDisplay();
        UpdateWaveDisplay();
    }

    private void UpdateAmmoDisplay()
    {
        if (weaponManager != null && ammoText != null)
        {
            int ammoInMag = weaponManager.GetAmmoInMagazine;
            int totalAmmo = weaponManager.GetTotalAmmo;
            ammoText.text = $"{ammoInMag} / {totalAmmo}";
        }
    }

    private void UpdateHealthDisplay()
    {
        if (playerHealth != null)
        {
            float health = playerHealth.GetHealth;
            float maxHealth = playerHealth.GetMaxHealth;
            
            if (healthText != null)
            {
                healthText.text = $"HP: {health:F0} / {maxHealth}";
            }
            
            if (healthBar != null)
            {
                healthBar.fillAmount = health / maxHealth;
            }
        }
    }

    private void UpdateWaveDisplay()
    {
        if (waveManager != null)
        {
            if (waveText != null)
            {
                waveText.text = $"Wave: {waveManager.GetCurrentWave}";
            }
            
            if (enemyCountText != null)
            {
                enemyCountText.text = $"Enemies: {waveManager.GetActiveEnemyCount}";
            }
        }
    }
}