using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy")]
    [SerializeField] private int _health = 10;
    public int Health => _health;

    public void Damage(int baseDamage)
    {
        _health -= baseDamage;

        if(_health <= 0)
        {
            _health = 0;
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
