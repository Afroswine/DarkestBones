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

    private void EnterPlayerTurnCombatState()
    {
        SM.ChangeState<PlayerTurnCombatState>();
    }

    private void EnterEnemyTurnCombatState()
    {
        SM.ChangeState<EnemyTurnCombatState>();
    }

    protected void EnterNextCombatState()
    {
        // if the fastest character is a hero...
        if (SM.Turn.CharacterOrder[SM.Turn.CurrentTurn].GetComponent<Hero>() != null)
        {
            //Debug.Log("It's a hero!");
            EnterPlayerTurnCombatState();
        }
        else if (SM.Turn.CharacterOrder[SM.Turn.CurrentTurn].GetComponent<Enemy>() != null)
        {
            //Debug.Log("It's an enemy!");
            EnterEnemyTurnCombatState();
        }
        else
        {
            Debug.LogWarning("TurnOrder.CharactersTurnOrder[] contains a non hero/enemy!");
        }
    }
}
