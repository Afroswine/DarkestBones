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

    private bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        _enemyTurnTextUI.gameObject.SetActive(true);
        EnemyTurnBegan?.Invoke();

        _enemyTurnTextUI.text = "...Enemy Thinking...";
        _enemyTurnTextUI.text += "\nWaiting for enemy turn end.";
        _enemyTurnTextUI.text += "\nBackSpace: Enter LoseState.";

        //SM.Input.PressedConfirm += OnPressedConfirm;
        SM.Input.PressedCancel += OnPressedCancel;
        //SM.Input.PressedSwap += OnPressedCancel;

        SM.Turn.ChangedTurn += EnterNextCombatState;
        SM.UI.EnableButtons(false);
        _activated = false;
        _cancelThinking = false;
    }

    public override void Tick()
    {
        if (!_activated)
        {
            _activated = true;
            StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
        }
    }

    public override void Exit()
    {
        _enemyTurnTextUI.gameObject.SetActive(false);
        Debug.Log("Enemy Turn: Exit...");
        _activated = false;

        //SM.Input.PressedConfirm -= OnPressedConfirm;
        SM.Input.PressedCancel -= OnPressedCancel;

        SM.Turn.ChangedTurn -= EnterNextCombatState;
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter player turn state.");
        SM.ChangeState<PlayerTurnCombatState>();
    }

    void OnPressedCancel()
    {
        Debug.Log("Attempt to enter lose combat state.");
        _cancelThinking = true;
        SM.ChangeState<LoseCombatState>();
    }

    private bool _cancelThinking = false;
    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy Thinking...");
        yield return new WaitForSeconds(pauseDuration);
        Debug.Log("Enemy performs action");
        if (!_cancelThinking)
        {
            EnemyTurnEnded?.Invoke();
            // turn over. Go back to player.
            //SM.ChangeState<PlayerTurnCombatState>();
            SM.Turn.NextTurn();
        }
        
        //_activated = false;
    }
}
