using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

// add the active characters for this round, and set their turn order
public class TurnController : MonoBehaviour
{
    // public
    public List<Character> TurnOrder => _turnOrder;

    // events
    public event Action ChangedTurn = delegate { };
    //public event Action ChangedRound = delegate { };

    // private
    [Tooltip("visually indicates which character's turn it is")]
    [SerializeField] TurnIndicator _turnIndicator;

    [Tooltip("A sorted list of the characters based on their speed.")]
    private List<Character> _turnOrder = new List<Character>();
    [Tooltip("Stores all (TODO - living and active) characters.")]
    private List<Character> _activeCharacters = new List<Character>();

    public void AddCharacter(List<Character> characters)
    {
        _activeCharacters.AddRange(characters);
        //characters.ForEach(i => Debug.Log("AddCharacterList() "+i.name));
    }
    public void AddCharacter(Character character)
    {
        _activeCharacters.Add(character);
        //Debug.Log("AddCharacter(): "+character.name);
    }

    public void RemoveCharacter(List<Character> characters)
    {
        foreach(var character in characters)
        {
            _activeCharacters.Remove(character);
        }
    }
    public void RemoveCharacter(Character character)
    {
        _activeCharacters.Remove(character);
    }

    // sets the turn order of the added characters
    public void Setup()
    {
        CreateTurnIndicator();
        NextRound();
    }

    private void CreateTurnIndicator()
    {
        if (_turnIndicator == null)
            return;

        _turnIndicator.TurnController = this;
        _turnIndicator = Instantiate(_turnIndicator, this.transform);
    }

    // sorts all remaining characters in '_turnOrder' by their speed, from highest to lowest
    private void UpdateTurnOrder()
    {
        _turnOrder.Sort((x, y) => y.Stats.Speed.CompareTo(x.Stats.Speed));
    }

    // remove the first character from '_turnOrder' (the character who just went) and call
    // 'UpdateTurnOrder()' in case any speed values have changed, invoke 'ChangedTurn'
    public void NextTurn()
    {
        if (TurnOrder.Count <= 1)   // if all characters had their turn this round (had turn = removed from turnOrder)
        {
            NextRound();
            return;
        }

        _turnOrder.RemoveAt(0);
        UpdateTurnOrder();
        ChangedTurn?.Invoke();
    }

    // refill '_turnOrder' with the active characters in the scene, 
    private void NextRound()
    {
        _turnOrder = new List<Character>(_activeCharacters);
        UpdateTurnOrder();
        foreach (var character in _turnOrder) Debug.Log("TurnOrder: " + character.name + ", Speed: " + character.Stats.Speed);
        ChangedTurn?.Invoke();
    }

    // gets the character of the current turn
    public Character CurrentCharacter()
    {
        return TurnOrder[0];
    }
}
