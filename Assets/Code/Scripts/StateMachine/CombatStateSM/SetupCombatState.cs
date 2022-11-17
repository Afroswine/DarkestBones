using UnityEngine;
using System;

public class SetupCombatState : CombatState
{
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("CombatSetup: ...Entering");
        //SM.Turn.ChangedTurn += Activate;

        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        SM.UI.EnableButtons(false);
        _activated = false;
    }

    public override void Tick()
    {
        // admittedly hacky for demo. You would usually have delays or Input.
        if (!_activated)
        {
            _activated = true;
            SM.Turn.AddCharacters(SM.HeroParty.GetPartyMembers());
            SM.Turn.AddCharacters(SM.EnemyParty.GetPartyMembers());
            SM.Turn.InitializeTurns();

            EnterNextCombatState();
        }
    }

    public override void Exit()
    {
        _activated = false;
        //SM.Turn.ChangedTurn -= Activate;
        Debug.Log("CombatSetup: Exiting...");
    }

    private void Activate()
    {
        _activated = true;
    }
}