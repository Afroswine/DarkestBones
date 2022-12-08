using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartyController : MonoBehaviour
{
    public Party HeroParty => _heroParty;
    public Party EnemyParty => _enemyParty;
    public List<Character> ActiveCharacters => _activeCharacters;

    public event Action<List<Character>> CharactersUpdated;

    public bool Ready => _ready;

    [Header("Party Controller")]
    [SerializeField] Party _heroParty;
    [SerializeField] Party _enemyParty;

    [Header("All characters within the two parties.")]
    private List<Character> _activeCharacters;

    [Tooltip("Has ActiveCharacters been populated?")]
    private bool _ready = false;

    private void OnEnable()
    {
        _ready = false;
    }

    private void Start()
    {
        UpdateActiveCharacters();
    }

    public void UpdateActiveCharacters()
    {
        _activeCharacters = new List<Character>(_heroParty.PartyMembers);
        _activeCharacters.AddRange(_enemyParty.PartyMembers);

        foreach(Hero hero in HeroParty.PartyMembers)
        {
            hero.OpposingParty = _enemyParty;
            hero.ActiveCharacters = _activeCharacters;
            //hero.AbilityButtons
        }
        foreach(Enemy enemy in EnemyParty.PartyMembers)
        {
            enemy.OpposingParty = _heroParty;
            enemy.ActiveCharacters = _activeCharacters;
        }

        CharactersUpdated?.Invoke(_activeCharacters);
        _ready = true;
    }
}
