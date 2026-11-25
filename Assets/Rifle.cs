public class Rifle : Weapon
{
    public override bool IsAutomatic => true;

    protected override void Awake()
    {
        weaponName = "Rifle";
        damage = 12f;
        fireRate = 0.1f;
        magazineSize = 30;
        reloadTime = 2f;
        recoilAmount = 2.5f;
        base.Awake();
    }
}