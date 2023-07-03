using UnityEngine;

class RoundAmmo : IAmmoStorageType
{
    WeaponAmmoController _ammoController;
    float _eachRoundLoadTime;

    public void Init(WeaponAmmoController weaponAmmoController)
    {
        _ammoController = weaponAmmoController;
        _ammoController.currentRoundCountInWeapon = _ammoController.weaponRoundCapacity;
        _eachRoundLoadTime = _ammoController.reloadDuration / _ammoController.weaponRoundCapacity;
    }

    public void GetReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _ammoController.StartReloading(_eachRoundLoadTime);
        }

        if (Input.GetMouseButtonDown(0) && _ammoController.interruptableReload)
        {
            _ammoController.FinishReloadProcess();
        }
    }

    public void ReduceAmmo() => _ammoController.currentRoundCountInWeapon--;

    public void ReloadProcess()
    {
        _ammoController.totalRoundCount--;
        _ammoController.currentRoundCountInWeapon++;

        if (_ammoController.currentRoundCountInWeapon < _ammoController.weaponRoundCapacity && _ammoController.isReloading)
        {
            _ammoController.StartReloading(_eachRoundLoadTime);
        }
        else
        {
            _ammoController.FinishReloadProcess();
        }
    }

    public bool CheckIfHasAmmo()
    {
        return _ammoController.currentRoundCountInWeapon > 0;
    }
}