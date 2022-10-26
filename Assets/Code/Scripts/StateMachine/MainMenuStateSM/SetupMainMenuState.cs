using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMainMenuState : MainMenuState
{
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("MainMenuSetup: ...Entering");
        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;

        if(SceneLoader.Instance.GetActiveSceneName() != "MainMenuScene")
        {
            SceneLoader.Instance.LoadScene("MainMenuScene");
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("MainMenuSetup: Exiting...");
    }
}
