using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Transform ammoText;
    [SerializeField] private Transform healthText;
    [SerializeField] private Transform waveText;
    [SerializeField] private Transform enemyCountText;
    [SerializeField] private RectTransform healthBar;
    
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
            Debug.Log($"Ammo: {ammoInMag} / {totalAmmo}");
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
                Debug.Log($"Health: {health:F0} / {maxHealth}");
            }
            
            if (healthBar != null)
            {
                float fillAmount = health / maxHealth;
                healthBar.localScale = new Vector3(fillAmount, 1, 1);
            }
        }
    }

    private void UpdateWaveDisplay()
    {
        if (waveManager != null)
        {
            if (waveText != null)
            {
                Debug.Log($"Wave: {waveManager.GetCurrentWave}");
            }
            
            if (enemyCountText != null)
            {
                Debug.Log($"Enemies: {waveManager.GetActiveEnemyCount}");
            }
        }
    }
}
