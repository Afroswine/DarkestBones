using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCombatState : CombatState
{
    [SerializeField] Text _playerTurnTextUI = null;

    int _playerTurnCount = 0;

    private bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");

        SM.Turn.ChangedTurn += EnterNextCombatState;
        SM.UI.EnableButtons(true);
        _activated = false;
        
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        _playerTurnTextUI.text += "\nSkip: ChangeTurn.";
        _playerTurnTextUI.text += "\nBackSpace: Enter WinState.";
        // subscribe
        //SM.Input.PressedConfirm += OnPressedConfirm;
        SM.Input.PressedCancel += OnPressedCancel;
        //SM.Input.PressedSwap += OnPressedCancel;
        
    }

    public override void Tick()
    {
        if (!_activated)
        {
            _activated = true;
            if(SM.Turn.GetCurrentTurnCharacter().TryGetComponent<Hero>(out Hero hero))
            {
                SM.UI.SetHero(hero);
            }
        }
    }

    public override void Exit()
    {
        
        _playerTurnTextUI.gameObject.SetActive(false);
        // unsubscribe
        //SM.Input.PressedConfirm -= OnPressedConfirm;
        SM.Input.PressedCancel -= OnPressedCancel;
        
        _activated = false;
        SM.UI.EnableButtons(false);
        SM.Turn.ChangedTurn += EnterNextCombatState;

        Debug.Log("Player Turn: Exiting...");
    }

    void OnChangedTurn()
    {
        if (SM.Turn.GetCurrentTurnCharacter().TryGetComponent<Hero>(out Hero hero))
        {
            SM.UI.SetHero(hero);
        }
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter Enemy State!");
        // change the enemy turn state
        SM.ChangeState<EnemyTurnCombatState>();
    }

    private void OnPressedCancel()
    {
        Debug.Log("Attempt to enter Win State!");
        SM.ChangeState<WinCombatState>();
    }
}