using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadScene : Button
{
    [Header("Button Load Scene")]
    [SerializeField] string _sceneName = "";

    protected override void ButtonPress()
    {
        if (_sceneName != "")
        {
            SceneLoader.Instance.LoadScene(_sceneName);
        }
        else
        {
            Debug.LogWarning("Not given a scene to load!");
        }
        
    }
}
