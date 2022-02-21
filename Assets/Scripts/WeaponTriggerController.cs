using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponFireMode 
{
    void Init(WeaponTriggerController weaponTriggerController);
    void GetTriggerInput();
    void OnTriggerCooldownEndedHandler();
}

public class WeaponTriggerController : MonoBehaviour
{
    public Action OnTriggerPerform;
    public Action OnTriggerCooldownEnded;
    public Action OnFireModeChanged;

    public float fireRate;
   
    public bool onFireCooldown;

    //make it scriptable object
    public bool isCapableBurstMode;
    public bool isCapableSemiAutoMode;
    public bool isCapableFullAutoMode;

    public List<IWeaponFireMode> capableFireModes = new List<IWeaponFireMode>();
    public int fireModeIndex;

    public IWeaponFireMode currentFireMode;
 
    [Header("Burst Trigger Properties")]
    public int burstShootCount;
    public int currentBurstPhase;
    [Header("Trigger Caching")]
    public bool isTriggerCached;

    private void Awake()
    {
        if (isCapableFullAutoMode)
            capableFireModes.Add(new FullAutoTrigger());

        if (isCapableSemiAutoMode)
            capableFireModes.Add(new SemiAutoTrigger());

        if (isCapableBurstMode)
            capableFireModes.Add(new BurstTrigger());

        currentFireMode = capableFireModes[fireModeIndex];       
        currentFireMode.Init(this);
    }

    private void Update()
    {
        currentFireMode.GetTriggerInput();

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            SwitchFireMode();
        }
    }

    public void PerformTrigger() 
    {
        if (onFireCooldown) return;

        onFireCooldown = true;
        isTriggerCached = false;
        OnTriggerPerform?.Invoke();
        StartCoroutine(TriggerCooldown());
    }

    void SwitchFireMode() 
    {
        fireModeIndex = (fireModeIndex + 1) % capableFireModes.Count;
        currentFireMode = capableFireModes[fireModeIndex];
        currentFireMode.Init(this);
        OnFireModeChanged?.Invoke();
    }

    IEnumerator TriggerCooldown() 
    {
        yield return new WaitForSeconds(fireRate);        
        OnTriggerCooldownEndedHandler();     
    }

    void OnTriggerCooldownEndedHandler() 
    {
        onFireCooldown = false;
        OnTriggerCooldownEnded?.Invoke();
        currentFireMode.OnTriggerCooldownEndedHandler();
    }
}

class SemiAutoTrigger : IWeaponFireMode
{
    WeaponTriggerController _weaponTriggerController;
    
    public void Init(WeaponTriggerController weaponTriggerController) => _weaponTriggerController = weaponTriggerController;

    public void GetTriggerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CacheTrigger();
            _weaponTriggerController.PerformTrigger();         
        }     
    }

    void CacheTrigger() 
    {
        if(_weaponTriggerController.onFireCooldown && _weaponTriggerController.isTriggerCached == false)
          _weaponTriggerController.isTriggerCached = true;
    }

    public void OnTriggerCooldownEndedHandler() 
    {
        if (_weaponTriggerController.isTriggerCached)
            _weaponTriggerController.PerformTrigger();
    }
}

class FullAutoTrigger : IWeaponFireMode
{
    WeaponTriggerController _weaponTriggerController;
    
    public void Init(WeaponTriggerController weaponTriggerController) => _weaponTriggerController = weaponTriggerController;

    public void GetTriggerInput()
    {
        if (Input.GetMouseButton(0))
            _weaponTriggerController.PerformTrigger();
    }

    public void OnTriggerCooldownEndedHandler() { return; }
}

class BurstTrigger : IWeaponFireMode
{
    WeaponTriggerController _weaponTriggerController;
    
    public void Init(WeaponTriggerController weaponTriggerController) => _weaponTriggerController = weaponTriggerController;

    public void GetTriggerInput()
    {
        if (Input.GetMouseButtonDown(0))
            _weaponTriggerController.PerformTrigger();
    }

    public void OnTriggerCooldownEndedHandler()
    {
        if (_weaponTriggerController.currentBurstPhase < _weaponTriggerController.burstShootCount - 1)
        {
            _weaponTriggerController.currentBurstPhase++;
            _weaponTriggerController.PerformTrigger();
        }
        else
        {
            _weaponTriggerController.currentBurstPhase = 0;
        }
    }
}


