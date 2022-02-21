using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmoStorageType 
{
    void Init(WeaponAmmoController weaponAmmoController);
    void ReduceAmmo();
    void GetReloadInput();
    void ReloadProcess();
    bool CheckIfHasAmmo();
}

public class WeaponAmmoController : MonoBehaviour
{
    private WeaponStateController weaponMainController;

    public Action OnReloadingStarted;
    public Action OnReloadingFinished;
    
    public enum AmmoStorageType { Infinity, Primitive, Round, Magazine }
    public AmmoStorageType ammoStorageType;

    public IAmmoStorageType currentAmmoStorageType;

    public float reloadDuration;
    public bool isReloading;

    [Header("Primitive Ammo Properties")]
    public int totalPrimitiveAmmoCount;

    [Header("Round Ammo Properties")]
    public bool interruptableReload;
    public int weaponRoundCapacity;
    public int currentRoundCountInWeapon;
    public int totalRoundCount;

    [Header("Magazine Ammo Properties")]
    public float panicReloadPressThreshold;
    public float panicReloadDuration;
    public int magazineBulletCapacity;
    public int currentMagazineIndex;
    public List<int> magazines = new List<int>();

    private void Awake()
    {
        weaponMainController = GetComponent<WeaponStateController>();
        weaponMainController.OnShootPerformed += OnShootPerformedHandler;

        switch (ammoStorageType)
        {
            case AmmoStorageType.Infinity:
                currentAmmoStorageType = new InfinityAmmo();
                break;

            case AmmoStorageType.Primitive:
                currentAmmoStorageType = new PrimitiveAmmo();
                break;

            case AmmoStorageType.Round:
                currentAmmoStorageType = new RoundAmmo();
                break;

            case AmmoStorageType.Magazine:
                currentAmmoStorageType = new MagazineAmmo();
                break;
        }

        currentAmmoStorageType.Init(this);
    }
    
    private void Update()
    {
        currentAmmoStorageType.GetReloadInput();
    }

    public void OnShootPerformedHandler()
    {
        currentAmmoStorageType.ReduceAmmo();
    }

    public void StartReloading(float duration)
    {
        weaponMainController.ChangeState(typeof(ReloadState));
        OnReloadingStarted?.Invoke();
        isReloading = true;
        StartCoroutine(ReloadingTimer(duration));
    }

    public IEnumerator ReloadingTimer(float duration) 
    {       
        yield return new WaitForSeconds(duration);
        ReloadProcess();
    }

    public void ReloadProcess() => currentAmmoStorageType.ReloadProcess();

    public void FinishReloadProcess()
    {
        weaponMainController.ChangeState(typeof(IdleState));
        isReloading = false;
        StopCoroutine("ReloadingTimer");  
        OnReloadingFinished?.Invoke();
    }

    public bool CheckIfHasAmmo() 
    {
        return currentAmmoStorageType.CheckIfHasAmmo();
    }
}







