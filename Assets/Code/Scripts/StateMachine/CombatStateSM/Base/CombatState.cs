using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CombatSM))]
public class CombatState : State
{
    // state machine
    protected CombatSM SM { get; private set; }

    private void Awake()
    {
        SM = GetComponent<CombatSM>();
    }

    private void OnEnable()
    {
        SM.PartyController.CharactersUpdated += UpdateActiveCharacters;
        SM.TurnController.ChangedTurn += EnterNextCombatState;
    }

    private void OnDisable()
    {
        SM.TurnController.ChangedTurn -= EnterNextCombatState;
        SM.PartyController.CharactersUpdated -= UpdateActiveCharacters;
    }

    // changes combat state based on the current turn's character
    private void EnterNextCombatState()
    {
        if (SM.TurnController.CurrentCharacter().GetComponent<Hero>() != null)
            SM.ChangeState<PlayerTurnCombatState>();
        else if (SM.TurnController.CurrentCharacter().GetComponent<Enemy>() != null) 
            SM.ChangeState<EnemyTurnCombatState>();
        else
            Debug.LogWarning("TurnOrder.CharactersTurnOrder[] contains a non hero/enemy!");
    }

    // updates the list of characters in turn controller
    private void UpdateActiveCharacters(List<Character> characters)
    {
        SM.TurnController.UpdateActiveCharacters(characters);
    }

    protected void AbilityTargetSelection(Party targetParty, List<bool> isTargetables)
    {
        // nullcheck the targetParty
        if (targetParty == null)
        {
            Debug.LogWarning("CombatState.ActivateAbilityTargeting(): No party given!");
            return;
        }
        if (isTargetables.Count > 4)
        {
            Debug.LogWarning("CombatState.ActivateAbilityTargetSelection(): isTargetables.Count is greater than 4!");
        }

        // Ensures we aren't checking for more targets than there actually are
        int targetsToCheck = Mathf.Min(targetParty.PartyMembers.Count, isTargetables.Count);

        // enable AbilityTargeting on the specified targets
        for (int i = 0; i < targetsToCheck; i++)
        {
            if (isTargetables[i] == true)
                targetParty.PartyMembers[i].InvokeTargeted();
        }
    }
}
