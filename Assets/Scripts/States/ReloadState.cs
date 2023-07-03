using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : StateBase<WeaponStateController>
{

    private float _reloadTime;
    public bool isReloadingProcessCompleted;

    public ReloadState(WeaponStateController controller) : base(controller) 
    {
    
    }

    public override void OnStateEnter()
    {
        _reloadTime = 1.0f;
    }

    public override void OnStateExit()
    {
        Debug.Log("Reload State Exit");
     
        if (isReloadingProcessCompleted)
        {
            controller._ammoController.FinishReloadProcess();
        }
        else 
        {
        // Cancel Reload
        }    
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateLateUpdate()
    {
       
    }

    public override void OnStateUpdate()
    {
        if (_reloadTime > 0.0f)
        {
            _reloadTime -= Time.deltaTime;
        }
        else 
        {
            isReloadingProcessCompleted = true;
            controller.ChangeState(typeof(IdleState));
        }
    }
}
