using UnityEngine;

public class SetupCombatState : CombatState
{
    [SerializeField] int _numberOfEnemies = 2;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("CombatSetup: ...Entering");
        Debug.Log("Creating " + _numberOfEnemies + " enemies.");
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;
    }

    public override void Tick()
    {
        // admittedly hacky for demo. You would usually have delays or Input.
        
        if(_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCombatState>();
        }
        
    }

    public void EnterPlayerTurnCombatState()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCombatState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("CombatSetup: Exiting...");
    }
}