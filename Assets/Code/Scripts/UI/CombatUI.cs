using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    [Header("CombatUI")]
    [SerializeField] SpriteRenderer _portrait;
    [Header("Abilities")]
    [SerializeField] Button _abilityButton1;
    [SerializeField] Button _abilityButton2;
    [SerializeField] Button _abilityButton3;
    [SerializeField] Button _abilityButton4;
    [SerializeField] Button _swapButton;
    [SerializeField] Button _skipButton;

    public void SetHero(Hero hero)
    {
        // set the ability buttons based on the given hero
        // set the sprite of the spriteRenderer
    }
}
