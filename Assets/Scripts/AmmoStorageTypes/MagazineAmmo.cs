using UnityEngine;

class MagazineAmmo : IAmmoStorageType
{
    WeaponAmmoController _weaponAmmoController;
    delegate void ReloadOperation();
    ReloadOperation reloadOperation;
    float pressTime;

    public void Init(WeaponAmmoController weaponAmmoController)
    {
        _weaponAmmoController = weaponAmmoController;
    }

    public void GetReloadInput()
    {
        if (_weaponAmmoController.magazines.Count < 1) return;

        GetPanicReloadInput();
        GetCalmReloadInput();
    }

    void GetPanicReloadInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            pressTime += Time.deltaTime;

            if (pressTime > _weaponAmmoController.panicReloadPressThreshold)
            {
                reloadOperation = DropMagazine;
                _weaponAmmoController.StartReloading(_weaponAmmoController.panicReloadDuration);
                pressTime = 0;
            }
        }
    }

    void GetCalmReloadInput()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (pressTime < _weaponAmmoController.panicReloadPressThreshold)
            {
                reloadOperation = SwitchMagazine;
                _weaponAmmoController.StartReloading(_weaponAmmoController.reloadDuration);
            }

            pressTime = 0;
        }
    }

    public void ReduceAmmo() => _weaponAmmoController.magazines[_weaponAmmoController.currentMagazineIndex]--;

    public void ReloadProcess()
    {
        reloadOperation();
    }

    void SwitchMagazine()
    {
        _weaponAmmoController.currentMagazineIndex = (_weaponAmmoController.currentMagazineIndex + 1) % _weaponAmmoController.magazines.Count;
    }

    void DropMagazine()
    {
        _weaponAmmoController.magazines.RemoveAt(_weaponAmmoController.currentMagazineIndex);
    }

    public bool CheckIfHasAmmo()
    {
        return _weaponAmmoController.magazines[_weaponAmmoController.currentMagazineIndex] > 0;
    }
}