class PrimitiveAmmo : IAmmoStorageType
{
    WeaponAmmoController _ammoController;

    public void Init(WeaponAmmoController weaponAmmoController)
    {
        _ammoController = weaponAmmoController;
    }

    public void ReduceAmmo() => _ammoController.totalPrimitiveAmmoCount--;

    public void ReloadProcess() { return; }

    public bool CheckIfHasAmmo()
    {
        return _ammoController.totalPrimitiveAmmoCount > 0;
    }

    public void GetReloadInput() { return; }
}