using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : StateBase<WeaponStateController>
{
    public ReloadState(WeaponStateController controller) : base(controller) 
    {
    
    }

    public override void OnStateEnter()
    {
        Debug.Log("Reload State Enter");
    }

    public override void OnStateExit()
    {
        Debug.Log("Reload State Exit");
        
    }

    public override void OnStateFixedUpdate()
    {
        
    }

    public override void OnStateLateUpdate()
    {
       
    }

    public override void OnStateUpdate()
    {
        
    }
}
