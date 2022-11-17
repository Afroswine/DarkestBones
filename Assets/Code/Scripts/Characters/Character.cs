using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHealable
{
    public int Health => _health;
    public int Speed => _speed;
    public bool TurnCompleted => _turnCompleted;
    public int PartyPosition => _partyPosition;
    public Party Party;

    // Base Stats
    [Header("Character")]
    [Tooltip("Max/Base HP.")]
    [SerializeField] private int _baseHealth = 40;
    [Tooltip("Potency of this character's abilities.")]
    [SerializeField] private int _basePower = 6;
    [Tooltip("% Damage Reduction.")]
    [SerializeField] [Range(0f, 0.8f)] private float _baseDefense = 0.0f;
    [Tooltip("Determines turn priority.")]
    [SerializeField] private int _baseSpeed = 10;

    // Current Stats
    [Tooltip("The current health of this character.")]
    private int _health;
    [Tooltip("The current power of this character.")]
    private int _power;
    [Tooltip("The current defense of this character.")]
    private float _defense;
    [Tooltip("The current speed of this character.")]
    private int _speed;

    // Abilities... might need state machine that takes into account the character's position
    protected CombatAbility _ability1;
    protected CombatAbility _ability2;
    protected CombatAbility _ability3;
    protected CombatAbility _ability4;

    [Tooltip("Tracks if this character had their turn this round.")]
    private bool _turnCompleted = false;

    private int _partyPosition = 0;

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

    private void Start()
    {
        _health = _baseHealth;
        _power = _basePower;
        _defense = _baseDefense;
        _speed = _baseSpeed;
    }

    // Damage this character.
    public void Damage(int baseDamage)
    {
        int realDamage = baseDamage - Mathf.FloorToInt(baseDamage * _defense);
        if (realDamage < 1)
            realDamage = 1;

        _health -= realDamage;

        if (_health <= 0)
        {
            _health = 0;
            Kill();
        }
    }

    // Heal this character.
    public void Heal(int baseHeal)
    {
        int realHeal = baseHeal;
        if (realHeal < 1)
            realHeal = 1;

        // Ensure _health doesn't go above its maximum
        _health = _health + realHeal >= _baseHealth ? _baseHealth : _health + realHeal;
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
