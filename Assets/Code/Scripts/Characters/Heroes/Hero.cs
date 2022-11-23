using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Hero : Character
{
    public Sprite Portrait => _portrait;
    public List<Button> AbilityButtonsList => _abilityButtonsList;
    
    [Header("Hero")]
    [SerializeField] Sprite _portrait;
    [SerializeField] List<Button> _abilityButtonsList = new(4);

    private void OnValidate()
    {
        if (_abilityButtonsList.Count != 4)
        {
            Debug.LogWarning("Must have " + 4 + " Abilities!");
            _abilityButtonsList.Resize(4);
        }
    }
}
