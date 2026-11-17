public class Pistol : Weapon
{
    protected override void Awake()
    {
        weaponName = "Pistol";
        damage = 25f;
        fireRate = 0.35f;
        magazineSize = 12;
        reloadTime = 1.2f;
        base.Awake();
    }
}