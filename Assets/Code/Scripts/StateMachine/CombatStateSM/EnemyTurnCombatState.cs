using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyTurnCombatState : CombatState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] Text _enemyTurnTextUI;
    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        _enemyTurnTextUI.gameObject.SetActive(true);
        EnemyTurnBegan?.Invoke();

        _enemyTurnTextUI.text = "...Enemy Thinking...";
        _enemyTurnTextUI.text += "\nSpace: Enter PlayerTurnCombatState.";
        _enemyTurnTextUI.text += "\nEscape: Enter LoseState.";

        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;
        //StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        _enemyTurnTextUI.gameObject.SetActive(false);
        Debug.Log("Enemy Turn: Exit...");

        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedCancel -= OnPressedCancel;
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter player turn state.");
        StateMachine.ChangeState<PlayerTurnCombatState>();
    }

    void OnPressedCancel()
    {
        Debug.Log("Attempt to enter lose combat state.");
        StateMachine.ChangeState<LoseCombatState>();
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy Thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        EnemyTurnEnded?.Invoke();
        // turn over. Go back to player.
        StateMachine.ChangeState<PlayerTurnCombatState>();
    }
}
