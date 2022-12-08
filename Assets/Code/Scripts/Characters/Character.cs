using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour, IHealable
{
    // public variables
    public CharacterStats Stats => _stats;
    public AbilityTargeting TargetSelect => _targetSelect;
    public List<Ability> Abilities => _abilities;
    public int Health => Stats.Health;
    public int PartyPosition => _partyPosition;
    public bool IsTurn = false;
    [HideInInspector] public Party Party = null;
    [HideInInspector] public Party OpposingParty = null;
    [HideInInspector] public List<Character> ActiveCharacters = null;

    [Header("Character")]
    [SerializeField] private CharacterStats _stats; // getting the stats component in awake did not work, doing this for now.
    [SerializeField] private List<Ability> _abilities;

    //private 
    private AbilityTargeting _targetSelect;
    private int _partyPosition = 0;

    // Events
    [Tooltip("Is this character ABLE TO BE targeted by an ability?")]
    public event Action Targeted = delegate { };

    // Cap party size
    private void OnValidate()
    {
        if (_partyPosition > Party.MAXCHARACTERS-1)
        {
            Debug.LogWarning("Party position may not be greater than: " + (Party.MAXCHARACTERS - 1)+"!");
        }
        if (_partyPosition < 0)
        {
            Debug.LogWarning("Party position may not be less than: 0!");
        }
    }

    private void Awake()
    {
        _targetSelect = GetComponentInChildren<AbilityTargeting>();
    }

    private void Start()
    {
        _targetSelect = GetComponentInChildren<AbilityTargeting>();
        //Debug.Log(_targetSelect.name);
    }

    public void InvokeTargeted()
    {
        _targetSelect = GetComponentInChildren<AbilityTargeting>();
        Targeted?.Invoke();
    }

    // Damage this character.
    public void Damage(int baseDamage)
    {
        baseDamage = Mathf.Abs(baseDamage);
        int realDamage = baseDamage - Mathf.FloorToInt(baseDamage * _stats.Defense);
        if (realDamage < 1) realDamage = 1;

        _stats.DecreaseHealth(realDamage);
        
        if(_stats.Health <= 0)
        {
            Kill();
        }
    }

    // Heal this character.
    public void Heal(int baseHeal)
    {
        int realHeal = baseHeal;
        if (realHeal < 1) realHeal = 1;

        _stats.IncreaseHealth(realHeal);
    }

    // Kill this character.
    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Reposition(int newPosition)
    {
        _partyPosition = newPosition;
    }
}
