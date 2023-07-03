using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponAmmoController))]
[RequireComponent(typeof(WeaponTriggerController))]
[RequireComponent(typeof(WeaponProjectileController))]
public class WeaponStateController : MonoBehaviour
{
    [NonSerialized] private Dictionary<Type, StateBase<WeaponStateController>> _possibleWeaponStates;
    [NonSerialized] private StateBase<WeaponStateController> _currentState;
    
    [SerializeField] private string _currentStateName;
 
    public WeaponAmmoController _ammoController;
    public WeaponTriggerController _triggerController;
    public WeaponProjectileController _attackController;

    public Action OnShootPerformed;
    public Action OnBulletLoaded;

    private void OnEnable()
    {
        _triggerController.OnTriggerPerform += OnTriggerPerformHandler;
    }

    private void OnDisable()
    {
        _triggerController.OnTriggerPerform -= OnTriggerPerformHandler;
    }

    private void Awake()
    {
        RegisterPossibleStates();
        SetInitialState();
    }

    private void RegisterPossibleStates() 
    {
        _possibleWeaponStates = new Dictionary<Type, StateBase<WeaponStateController>>
        {
            {typeof(EquipState), new EquipState(this)},
            {typeof(IdleState), new IdleState(this)},
            {typeof(ReloadState), new ReloadState(this)},
            {typeof(UnEquipState), new UnEquipState(this) }
        };
    }

    void SetInitialState()
    {
        _currentState = GetState(typeof(IdleState));
        _currentState.OnStateEnter();
    }

    public void ChangeState(Type nextState) 
    {
        if (_currentState.GetType() != nextState)
        {
            _currentState.OnStateExit();
            _currentState = GetState(nextState);
            _currentState.OnStateEnter();
        }
    }

    public StateBase<WeaponStateController> GetState(Type type) 
    {
        return _possibleWeaponStates[type];
    }

    private void Update()
    {
        _currentState.OnStateUpdate();
        _currentStateName = _currentState.ToString();
    }

    private void LateUpdate()
    {
        _currentState.OnStateLateUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnStateFixedUpdate();
    }

    public void OnTriggerPerformHandler() 
    {
        if (_currentState != GetState(typeof(IdleState)) || !_ammoController.CheckIfHasAmmo()) 
            return;
       
        _attackController.PerformShoot();
        OnShootPerformed?.Invoke();
    }
}
