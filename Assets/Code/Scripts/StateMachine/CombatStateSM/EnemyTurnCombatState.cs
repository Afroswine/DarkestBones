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
        Debug.Log("Enemy Turn: ...Entering");
        SM.Input.PressedCancel += OnPressedCancel;
        
        SM.UI.EnableButtons(false);
        _activated = false;
        _enemyTurnTextUI.gameObject.SetActive(true);

        _enemyTurnTextUI.text = "Enemy Turn";

        EnemyTurnBegan?.Invoke();
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
        Debug.Log("Enemy Turn: Exiting...");
        _activated = false;

        SM.Input.PressedCancel -= OnPressedCancel;

        EnemyTurnEnded?.Invoke();
    }

    void OnPressedCancel()
    {
        Debug.Log("Attempt to enter lose combat state.");
        SM.ChangeState<LoseCombatState>();
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        //Debug.Log("Enemy Thinking...");
        yield return new WaitForSeconds(pauseDuration);
        //Debug.Log("Enemy performs action");
        _activated = false;
        SM.TurnController.NextTurn();
    }
}
