using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnEquipState : StateBase<WeaponStateController>
{

    public UnEquipState(WeaponStateController controller) : base(controller) 
    {
    
    }

    public override void OnStateEnter()
    {
        Debug.Log("UnEquip State Enter");
    }

    public override void OnStateExit()
    {
        Debug.Log("UnEquip State Exit");
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
