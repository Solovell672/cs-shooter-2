using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Gun currentGun;
    [SerializeField] private Transform gunMuzzle;
    private bool isFiring = false;

    private void Start()
    {
        if (currentGun == null)
            currentGun = GetComponentInChildren<Gun>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isFiring && currentGun.HasAmmo())
            {
                StartCoroutine(FireBurst());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            currentGun.Reload();
        }
    }

    private System.Collections.IEnumerator FireBurst()
    {
        isFiring = true;
        int bulletsInBurst = 3;

        for (int i = 0; i < bulletsInBurst; i++)
        {
            if (currentGun.HasAmmo())
            {
                currentGun.Fire(gunMuzzle.position, gunMuzzle.forward);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                break;
            }
        }

        isFiring = false;
        yield return new WaitForSeconds(0.3f);
    }

    public Gun GetCurrentGun() => currentGun;
}