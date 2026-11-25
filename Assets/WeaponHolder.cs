using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    private int current;
    public Weapon CurrentWeapon => weapons.Length > 0 ? weapons[current] : null;

    void Start() { Equip(0); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(1);

        Weapon w = weapons[current];
        if (Input.GetKeyDown(KeyCode.R)) w.StartReload();

        bool fire = w.IsAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        if (fire && Time.timeScale > 0) w.TryFire();
    }

    void Equip(int index)
    {
        if (index >= weapons.Length) return;
        current = index;
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == index);
    }
}