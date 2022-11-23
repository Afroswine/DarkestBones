using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [Header("CombatUI")]
    [SerializeField] SpriteRenderer _portraitRenderer;
    [Header("Ability Buttons")]
    [SerializeField] List<Button> _abilities = new(4);
    [SerializeField] Button _swap;
    [SerializeField] Button _skip;
    
    private List<Button> _buttonList = new(6);

    private void OnValidate()
    {
        if (_abilities.Count != 4)
        {
            _abilities.Resize(4);
            Debug.LogWarning("There must be 4 Ability Buttons!");
        }
    }

    private void Start()
    {
        _buttonList = new(_abilities);
        _buttonList.Add(_swap);
        _buttonList.Add(_skip);
    }

    public void EnableButtons(bool isEnabled)
    {
        foreach(var button in _buttonList)
        {
            button.Enable(isEnabled);
        }
    }

    private void SetAbilities(List<Button> newButtons)
    {
        for(int i = 0; i < newButtons.Count; i++)
        {
            if(newButtons[i] != null)
            {
                _abilities[i].Set(newButtons[i]);
            }
        }
    }

    public void SetHero(Hero hero)
    {
        _portraitRenderer.sprite = hero.Portrait;
        SetAbilities(hero.AbilityButtonsList);
    }
}
