using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCombatState : CombatState
{
    [SerializeField] Text _loseTextUI;

    public override void Enter()
    {
        Debug.Log("LoseCombat: ...Entering");
        _loseTextUI.gameObject.SetActive(true);

        _loseTextUI.text = "Lose Win";
        _loseTextUI.text += "\nSpace: Enter MainMenuState";
        _loseTextUI.text += "\nEscape: Enter SetupCombatState";

        SM.Input.PressedConfirm += OnPressedConfirm;
        SM.Input.PressedCancel += OnPressedCancel;
    }

    public override void Exit()
    {
        Debug.Log("LoseCombat: Exiting...");
        _loseTextUI.gameObject.SetActive(false);

        SM.Input.PressedConfirm -= OnPressedConfirm;
        SM.Input.PressedCancel -= OnPressedCancel;
    }

    private void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter MainMenuState");
        //MainMenuStateMachine.ChangeState<SetupMainMenuState>();
        SceneLoader.Instance.LoadScene("MainMenuScene");
    }

    private void OnPressedCancel()
    {
        Debug.Log("Attempt to enter SetupCombatState");
        SM.ChangeState<SetupCombatState>();
    }
}
