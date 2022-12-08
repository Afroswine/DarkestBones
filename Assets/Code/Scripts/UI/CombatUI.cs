using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [Header("CombatUI")]
    [SerializeField] SpriteRenderer _portraitRenderer;
    [Header("Ability Buttons")]
    [SerializeField] List<AbilityButton> _abilities = new(4);
    [SerializeField] AbilityButton _swap;
    [SerializeField] AbilityButton _skip;

    private List<AbilityButton> _abilityButtons = new(6);

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
        _abilityButtons = new(_abilities);
        _abilityButtons.Add(_swap);
        _abilityButtons.Add(_skip);
    }

    public void EnableButtons(bool isEnabled)
    {
        foreach(var button in _abilityButtons)
        {
            button.Enable(isEnabled);
        }
    }

    private void SetAbilities(Hero hero, List<Ability> abilities)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i] != null)
            {
                _abilities[i].SetAbility(hero, abilities[i]);
            }
        }

        _swap.SetAbility(hero);
        _skip.SetAbility(hero);
    }
    private void SetAbilities(Enemy enemy, List<Ability> abilities)
    {

    }

    public void SetHero(Hero hero)
    {
        _portraitRenderer.sprite = hero.Portrait;
        SetAbilities(hero, hero.Abilities);
    }
}
