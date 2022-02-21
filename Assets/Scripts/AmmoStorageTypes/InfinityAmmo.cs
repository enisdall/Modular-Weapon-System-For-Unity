class InfinityAmmo : IAmmoStorageType
{
    public void Init(WeaponAmmoController weaponAmmoController) { }

    public void ReduceAmmo() { return; }

    public void ReloadProcess() { return; }

    public bool CheckIfHasAmmo() { return true; }

    public void GetReloadInput() { return; }

}