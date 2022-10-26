using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeState : Button
{
    [Header("Button Change State")]
    [SerializeField] StateMachine _stateMachine;
    [SerializeField] State _newState;

    protected override void ButtonPress()
    {
        
    }
}
