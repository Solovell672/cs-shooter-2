using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int damage = 25;
    [SerializeField] private int maxAmmo = 120;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float bulletRange = 1000f;
    [SerializeField] private Transform firePoint;
    
    private int currentAmmo;
    private int ammoInMagazine;
    private float lastFireTime;
    private bool isReloading;

    private void Start()
    {
        currentAmmo = maxAmmo;
        ammoInMagazine = magazineSize;
    }

    private void Update()
    {
        HandleShooting();
        HandleReload();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastFireTime + fireRate && ammoInMagazine > 0 && !isReloading)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    private void Shoot()
    {
        ammoInMagazine--;
        
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, bulletRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoInMagazine < magazineSize && currentAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2f);
        
        int ammoNeeded = magazineSize - ammoInMagazine;
        int ammoToReload = Mathf.Min(ammoNeeded, currentAmmo);
        
        ammoInMagazine += ammoToReload;
        currentAmmo -= ammoToReload;
        isReloading = false;
    }

    public int GetAmmoInMagazine => ammoInMagazine;
    public int GetTotalAmmo => currentAmmo;
    public bool IsReloading => isReloading;
}