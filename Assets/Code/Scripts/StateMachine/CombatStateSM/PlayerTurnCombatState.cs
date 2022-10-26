using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCombatState : CombatState
{
    [SerializeField] Text _playerTurnTextUI = null;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        _playerTurnTextUI.text += "\nSpace: Enter EnemyTurnCombatState.";
        _playerTurnTextUI.text += "\nEscape: Enter WinState.";
        // subscribe
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedCancel += OnPressedCancel;
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        // unsubscribe
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedCancel -= OnPressedCancel;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter Enemy State!");
        // change the enemy turn state
        StateMachine.ChangeState<EnemyTurnCombatState>();
    }

    private void OnPressedCancel()
    {
        Debug.Log("Attempt to enter Win State!");
        StateMachine.ChangeState<WinCombatState>();
    }
}