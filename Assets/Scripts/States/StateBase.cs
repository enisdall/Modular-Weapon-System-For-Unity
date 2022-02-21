using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase<T>
{
    public T controller;

    public StateBase(T controller) => this.controller = controller;

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateLateUpdate();

    public abstract void OnStateFixedUpdate();

    public abstract void OnStateExit();     
}
