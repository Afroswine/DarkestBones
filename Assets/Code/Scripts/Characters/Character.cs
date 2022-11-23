using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Character : MonoBehaviour, IHealable
{
    public CharacterStats Stats => _stats;
    public int Health => Stats.Health;
    public int PartyPosition => _partyPosition;
    [HideInInspector]
    public Party Party = null;

    // Abilities... might need state machine that takes into account the character's position
    protected CombatAbility _ability1;
    protected CombatAbility _ability2;
    protected CombatAbility _ability3;
    protected CombatAbility _ability4;

    private int _partyPosition = 0;

    [SerializeField] private CharacterStats _stats; // getting the stats component in awake did not work, doing this for now.
    //private Party _party;

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
