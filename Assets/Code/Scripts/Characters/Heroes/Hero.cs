using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Hero : Character
{
    public Sprite Portrait => _portrait;
    public Button[] AbilityButtons => _abilityButtons;
    
    [Header("Hero")]
    [SerializeField] Sprite _portrait;
    [SerializeField] Button[] _abilityButtons = new Button[4];

    private void OnValidate()
    {
        if (_abilityButtons.Length != 4)
        {
            Debug.LogWarning("Must have " + 4 + " Abilities!");
            Array.Resize(ref _abilityButtons, 4);
        }
    }
}
