using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateBase<WeaponStateController>
{

    public IdleState(WeaponStateController controller) : base(controller)
    {
    
    }

    public override void OnStateEnter()
    {
        Debug.Log("Idle State Enter");
    }

    public override void OnStateExit()
    {
        Debug.Log("Idle State Exit");
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
