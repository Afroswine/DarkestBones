using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCombatState : CombatState
{
    [SerializeField] Text _playerTurnTextUI = null;

    private bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        SM.Input.PressedCancel += OnPressedCancel;
        SM.TurnController.ChangedTurn += UpdateHeroGUI;

        UpdateHeroGUI();
        SM.UI.EnableButtons(true);
        _activated = false;
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerTurnTextUI.text = "Player Turn";
    }

    public override void Tick()
    {
        if (!_activated)
        {
            _activated = true;
        }
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        _activated = false;
        SM.UI.EnableButtons(false);

        SM.TurnController.ChangedTurn -= UpdateHeroGUI;
        SM.Input.PressedCancel -= OnPressedCancel;

        Debug.Log("Player Turn: Exiting...");
    }

    private void UpdateHeroGUI()
    {
        if (SM.TurnController.CurrentCharacter().TryGetComponent(out Hero hero))
        {
            SM.UI.SetHero(hero);
        }
    }

    private void OnPressedCancel()
    {
        Debug.Log("Attempt to enter Win State!");
        SM.ChangeState<WinCombatState>();
    }
}