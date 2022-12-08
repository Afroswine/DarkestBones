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
    }

    public override void Tick()
    {
        // admittedly hacky for demo. You would usually have delays or Input.
        if (_activated)
            return;

        if (!SM.PartyController.Ready) 
            return;
        SM.PartyController.HeroParty.InstantiateParty();
        SM.PartyController.EnemyParty.InstantiateParty();
        SM.PartyController.UpdateActiveCharacters();
        SM.TurnController.Setup();
        UpdateHeroGUI();

        _activated = true;
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("CombatSetup: Exiting...");
    }

    private void UpdateHeroGUI()
    {
        for(int i = 0; i < SM.TurnController.TurnOrder.Count; i++)
        {
            if(SM.TurnController.TurnOrder[i].TryGetComponent(out Hero firstHero))
            {
                SM.UI.SetHero(firstHero);
                return;
            }
        }
    }
}