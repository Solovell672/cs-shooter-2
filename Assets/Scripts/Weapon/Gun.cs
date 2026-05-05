using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] private int maxAmmo = 120;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private int damage = 25;
    [SerializeField] private Camera mainCamera;

    private void Start() {
        currentAmmo = maxAmmo;
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public bool HasAmmo() => currentAmmo > 0;

    public void Fire(Vector3 firePosition, Vector3 fireDirection) {
        if (!HasAmmo()) return;

        currentAmmo--;
        RaycastHit hit;

        if (Physics.Raycast(firePosition, fireDirection, out hit, fireRange)) {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                Debug.Log("💥 Попадание! Урон: " + damage);
            }
            Debug.Log("🎯 Выстрел в: " + hit.collider.name);
        }
    }

    public void Reload() {
        currentAmmo = maxAmmo;
        Debug.Log("🔄 Перезарядка! Патроны: " + currentAmmo);
    }

    public int GetCurrentAmmo() => currentAmmo;

    public int GetMaxAmmo() => maxAmmo;
}