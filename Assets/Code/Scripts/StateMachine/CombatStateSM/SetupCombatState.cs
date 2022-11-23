using UnityEngine;
using System;

public class SetupCombatState : CombatState
{
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("CombatSetup: ...Entering");

        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        SM.UI.EnableButtons(false);
        _activated = false;

        SM.HeroParty.InstantiateParty();
        SM.EnemyParty.InstantiateParty();
        SM.TurnController.AddCharacter(SM.HeroParty.GetPartyMembers());
        SM.TurnController.AddCharacter(SM.EnemyParty.GetPartyMembers());
    }

    public override void Tick()
    {
        // admittedly hacky for demo. You would usually have delays or Input.
        if (_activated)
            return;
        
        _activated = true;
        SM.TurnController.Setup();
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("CombatSetup: Exiting...");
    }
}