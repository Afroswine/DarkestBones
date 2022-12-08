using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCombatState : CombatState
{
    [SerializeField] Text _playerTurnTextUI = null;

    private bool _activated = false;

    #region CombatState Base
    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        // subscribe
        SM.Input.PressedCancel += OnPressedCancel;
        SM.TurnController.ChangedTurn += UpdateHeroGUI;

        // enable ability buttons
        SM.PartyController.UpdateActiveCharacters();
        UpdateHeroGUI();
        SM.UI.EnableButtons(true);

        _activated = false;

        // enable text object
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
        // disable text object
        _playerTurnTextUI.gameObject.SetActive(false);

        _activated = false;

        // disable ability buttons
        SM.UI.EnableButtons(false);

        // unsubscribe
        SM.TurnController.ChangedTurn -= UpdateHeroGUI;
        SM.Input.PressedCancel -= OnPressedCancel;

        Debug.Log("Player Turn: Exiting...");
    }
    #endregion CombatState Base END

    // changes the UI based on current hero
    private void UpdateHeroGUI()
    {
        if (SM.TurnController.CurrentCharacter().TryGetComponent(out Hero hero))
        {
            SM.UI.SetHero(hero);
        }
    }

    // TODO - remove
    private void OnPressedCancel()
    {
        Debug.Log("Attempt to enter Win State!");
        SM.ChangeState<WinCombatState>();
    }
}