using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Text healthText;
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private PlayerController playerController;
    private int playerHealth = 100;

    private void Start()
    {
        if (weaponSystem == null)
            weaponSystem = FindObjectOfType<WeaponSystem>();
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (weaponSystem != null)
        {
            Gun gun = weaponSystem.GetCurrentGun();
            if (gun != null && ammoText != null)
            {
                ammoText.text = "Патроны: " + gun.GetCurrentAmmo() + "/" + gun.GetMaxAmmo();
            }
        }

        if (healthText != null)
        {
            healthText.text = "Здоровье: " + playerHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("💀 Игрок погиб!");
        }
    }
}