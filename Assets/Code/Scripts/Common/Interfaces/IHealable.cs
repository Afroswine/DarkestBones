using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealable : IDamageable
{
    public void Heal(int baseHeal);
}
