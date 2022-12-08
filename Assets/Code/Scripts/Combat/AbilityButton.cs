using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityButton : Button
{
    [Space]
    [Header("Ability Button")]
    [SerializeField] Ability _ability;
    private void Start()
    {
        _ability = GetComponent<Ability>();
        _isEnabled = false;
    }


    public void SetAbility(Hero caster)
    {
        _isEnabled = true;
        GetComponent<Ability>().Caster = caster;

    }
    public void SetAbility(Hero caster, Ability ability)
    {
        _isEnabled = true;
        GetComponent<Ability>().Caster = caster;
        _ability = ability;
        _ability.Caster = caster;
        _sprites = new List<Sprite>(_ability.ButtonSprites);
        _spriteRenderer.sprite = _sprites[0];
    }

    protected override void ButtonPress()
    {
        _ability.HighlightTargets();
        base.ButtonPress();
    }
}
