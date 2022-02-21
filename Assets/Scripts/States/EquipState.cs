using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState : StateBase<WeaponStateController>
{

    public EquipState(WeaponStateController controller) : base(controller) 
    {
    
    }

    public override void OnStateEnter()
    {
        Debug.Log("Equip State Enter");
    }

    public override void OnStateExit()
    {
        Debug.Log("Equip State Enter");
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
