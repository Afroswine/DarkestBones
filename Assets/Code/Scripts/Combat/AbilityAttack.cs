using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class AbilityAttack : Ability
{
    [Space]
    [Header("Ability Attack")]
    [SerializeField] float _powerMultiplier = 1;
    [SerializeField] int _abilityAccuracy = 75;

    #region Ability Overrides
    public override void Cast()
    {
        if (!AccuracyCheck())
            return;

        if (_multiTarget)
        {
            foreach(Character target in _targets)
            {
                int damage = Mathf.RoundToInt(Caster.Stats.Power * _powerMultiplier);
                target.Damage(damage);
            }
        }
        else
        {
            int damage = Mathf.RoundToInt(Caster.Stats.Power * _powerMultiplier);
            _target.Damage(damage);
        }
    }

    public override void HighlightTargets()
    {
        
    }
    #endregion Ability Overrides END

    #region Attack Specific
    // TODO - after checking accuracy, check for dodge chance
    private bool AccuracyCheck()
    {
        int check = Mathf.RoundToInt(Random.Range(0, 100));
        
        if (check <= _abilityAccuracy + Caster.Stats.Accuracy)
            return true;

        // else
        Miss();
        return false;
    }

    private void Miss()
    {
        Debug.Log("Ability Attack(): Miss()");
    }

    private bool DodgeCheck()
    {
        // TODO - after checking for hit, check each target's dodge chance
        return false;
    }
    #endregion Attack Specific END
}
