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
    protected float recoilAmount = 1.5f;

    protected Camera cam;
    private float nextFireTime;
    private AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public GameObject bulletPrefab;
    public Transform muzzlePoint;

    public int CurrentAmmo { get; protected set; }
    public bool IsReloading { get; private set; }
    public string WeaponName => weaponName;
    public virtual bool IsAutomatic => false;

    protected virtual void Awake()
    {
        cam = GetComponentInParent<Camera>();
        CurrentAmmo = magazineSize;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.playOnAwake = false;
    }

    protected virtual void OnDisable() { IsReloading = false; }

    public void TryFire()
    {
        if (IsReloading || Time.time < nextFireTime) return;
        if (CurrentAmmo <= 0) { StartReload(); return; }

        nextFireTime = Time.time + fireRate;
        CurrentAmmo--;
        Debug.Log($"{weaponName} fired | Ammo: {CurrentAmmo}");
        if (fireSound) audioSource.PlayOneShot(fireSound);
        Recoil.Instance?.ApplyRecoil(recoilAmount);
        Shoot();
    }

        protected virtual void Shoot()
    {
        if (bulletPrefab != null && muzzlePoint != null)
        {
            GameObject b = Instantiate(bulletPrefab, muzzlePoint.position, cam.transform.rotation);
            b.GetComponent<Bullet>().damage = damage;
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
        if (reloadSound) audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmo = magazineSize;
        IsReloading = false;
    }
}