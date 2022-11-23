using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    private const float MAXDEFENSE = 0.8f;

    // PUBLIC base stats
    public int MaxHealth => _maxHealth;
    public int BasePower => _basePower;
    public float BaseDefense => _baseDefense;
    public int BaseSpeed => _baseSpeed;
    // PUBLIC current stats
    public int Health => _health;
    public int Power => _power;
    public float Defense => _defense;
    public int Speed => _speed;

    // base stats
    [Header("Character Stats")]
    [Space]
    [Header("Base")]
    [SerializeField] 
    private int _maxHealth = 40;
    [SerializeField] 
    private int _basePower = 6;
    [SerializeField] 
    private int _baseSpeed = 10;
    [SerializeField]
    [Range(0f, MAXDEFENSE)]
    private float _baseDefense = 0f;

    // current stats
    [Space]
    [Header("Current")]
    private int _health = 1;
    private int _power = 1;
    private float _defense = 1;
    private int _speed = 1;

    private void OnEnable() 
    {
        //_turnController. "some kind of event that tracks when a specific character ends their turn" 
    }

    // Resets current stats to their base values
    public void ResetStats()
    {
        _health = MaxHealth;
        _power = BasePower;
        _defense = BaseDefense;
        _speed = BaseSpeed;
    }

    #region Stat Increases
    public void IncreaseHealth(int amount)
    {
        if (_health + Mathf.Abs(amount) > _maxHealth)
        {
            _health = _maxHealth;
            return;
        }
        Increase(ref _health, amount);
    }
    #endregion Stat Increases END

    #region Stat Decreases
    public void DecreaseHealth(int amount)
    {
        Decrease(ref _health, amount);
    }
    #endregion Stat Decreases END

    // TODO - not very safe, references other than *THESE* stats could be passed through here and modified.
    private void Increase(ref int stat, int amount)
    {
        if (Mathf.Sign(amount) == Mathf.Sign(-1))
            Debug.Log("Attempted to increase stat by negative amount.");

        stat += Mathf.Abs(amount);
    }
    private void Increase(ref float stat, float amount)
    {
        if (Mathf.Sign(amount) == Mathf.Sign(-1))
            Debug.Log("Attempted to increase stat by negative amount.");

        stat += Mathf.Abs(amount);
    }

    // TODO - not very safe, references other than *THESE* stats could be passed through here and modified.
    private void Decrease(ref int stat, int amount)
    {
        if (Mathf.Sign(amount) == Mathf.Sign(-1))
            Debug.Log("Attempted to decrease stat by negative amount.");

        amount = Mathf.Abs(amount);
        if(stat - amount < 0)
        {
            stat = 0;
            return;
        }
        stat -= amount;
    }
    private void Decrease(ref float stat, float amount)
    {
        if (Mathf.Sign(amount) == Mathf.Sign(-1))
            Debug.Log("Attempted to decrease stat by negative amount.");

        amount = Mathf.Abs(amount);
        if(stat - amount < 0)
        {
            stat = 0;
            return;
        }

        stat -= Mathf.Abs(amount);
    }
}
