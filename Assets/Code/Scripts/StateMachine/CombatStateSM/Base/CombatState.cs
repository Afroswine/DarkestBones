using UnityEngine;

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
        SM.TurnController.ChangedTurn += EnterNextCombatState;
    }

    private void OnDisable()
    {
        SM.TurnController.ChangedTurn -= EnterNextCombatState;
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
}
