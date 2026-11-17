using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected string weaponName = "Weapon";
    protected float damage = 10f;
    protected float range = 100f;
    protected float fireRate = 0.3f;
    protected int magazineSize = 12;
    protected float reloadTime = 1.5f;

    protected Camera cam;
    private float nextFireTime;

    public int CurrentAmmo { get; protected set; }
    public bool IsReloading { get; private set; }
    public string WeaponName => weaponName;
    public virtual bool IsAutomatic => false;

    protected virtual void Awake()
    {
        cam = GetComponentInParent<Camera>();
        CurrentAmmo = magazineSize;
    }

    protected virtual void OnDisable() { IsReloading = false; }

    public void TryFire()
    {
        if (IsReloading || Time.time < nextFireTime) return;
        if (CurrentAmmo <= 0) { StartReload(); return; }

        nextFireTime = Time.time + fireRate;
        CurrentAmmo--;
        Debug.Log($"{weaponName} fired | Ammo: {CurrentAmmo}");
        Shoot();
    }

    protected virtual void Shoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            var target = hit.collider.GetComponentInParent<IDamageable>();
            target?.TakeDamage(damage);
            Debug.Log($"{weaponName} hit {hit.collider.name} | Ammo: {CurrentAmmo}");
        }
    }

    public void StartReload()
    {
        if (IsReloading || CurrentAmmo == magazineSize) return;
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        IsReloading = true;
        Debug.Log($"{weaponName} reloading...");
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmo = magazineSize;
        IsReloading = false;
    }
}