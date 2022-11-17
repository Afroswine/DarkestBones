using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatAbility : MonoBehaviour
{
    [Header("CombatAbility")]
    private IDamageable _target;

    [Tooltip("Set the target for this ability.")]
    public virtual void SetTarget(IDamageable target)
    {
        _target = target;
    }

    [Tooltip("Cast the ability on the target.")]
    public abstract void Cast();
}
