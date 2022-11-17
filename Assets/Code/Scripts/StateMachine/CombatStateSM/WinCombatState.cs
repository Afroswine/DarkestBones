using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCombatState : CombatState
{
    [SerializeField] Text _winTextUI;

    public override void Enter()
    {
        Debug.Log("WinCombat: ...Entering");
        _winTextUI.gameObject.SetActive(true);

        //_winTextUI.text = "You Win";
        _winTextUI.text += "\nSpace: Enter MainMenu";
        //_winTextUI.text += "\nBackSpace: Enter SetupCombatState";

        SM.Input.PressedConfirm += OnPressedConfirm;
        SM.Input.PressedCancel += OnPressedCancel;
    }

    public override void Exit()
    {
        Debug.Log("WinCombat: Exiting...");
        _winTextUI.gameObject.SetActive(false);

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
