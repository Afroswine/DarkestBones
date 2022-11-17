using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// add the active characters for this round, and set their turn order
public class TurnController : MonoBehaviour
{
    // access the list of characters organized by turn order
    public Character[] CharacterOrder => _characterOrder;
    public int CurrentTurn => _currentTurn;

    public event Action ChangedTurn = delegate { };

    [Header("TurnController")]
    [Header("TurnIndicator")]
    [SerializeField] GameObject _turnIndicatorSprite;
    [SerializeField] int _indicatorYOffset = 0;
    //private GameObject _turnIndicator;
    private bool _isDisplaying = false;

    [Tooltip("A sorted list of the characters based on their speed.")]
    private Character[] _characterOrder = new Character[0];
    [Tooltip("tracks the current character index for adding characters.")]
    private int _addCharacterIndex = 0;
    [Tooltip("The index of the character whose turn it currently is.")]
    private int _currentTurn = 0;

    private void Start()
    {
        _turnIndicatorSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        DisplayTurnIndicator(_isDisplaying);
    }

    // add characters to _allcharacters to find turn order
    public void AddCharacters(Character[] characters)
    {
        // resize _characterOrder to its length + characters.Length
        Array.Resize(ref _characterOrder, characters.Length + _characterOrder.Length);

        // add characters
        for(int i = 0; i < characters.Length; i++)
        {
            _characterOrder[_addCharacterIndex] = characters[i];
            Debug.Log(i + ": " + characters[i].name);
            _addCharacterIndex++;
        }
    }

    // sets the turn order of the added characters
    public void InitializeTurns()
    {
        // create a reference of the original character list
        Character[] characters = _characterOrder;

        // the index of the (current) fastest character
        int fastestIndex = 0;

        // Loop through the entire array once
        for(int i = 0; i < _characterOrder.Length; i++)
        {
            // Compare each element in the array to the other elements
            for (int j = fastestIndex; j < _characterOrder.Length; j++)
            {
                if (_characterOrder[i].Speed > characters[j].Speed)
                {
                    _characterOrder[i] = characters[j];
                }
                else
                {
                    fastestIndex++;
                }
            }
            Debug.Log("_charactersTurnOrder[" + i + "] = " + _characterOrder[i].name);
        }

        ChangedTurn?.Invoke();

        // Set initial turn index
        _currentTurn = 0;
        _isDisplaying = true;
    }

    private void DisplayTurnIndicator(bool isDisplaying)
    {
        if (isDisplaying)
        {
            Character character = _characterOrder[_currentTurn];
            Vector3 position = character.Party.Positions[character.PartyPosition];
            position.y += _indicatorYOffset;
            _turnIndicatorSprite.transform.position = position;
            _turnIndicatorSprite.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // increments _currentTurn
    public void NextTurn()
    {
        _currentTurn++;
        Debug.Log("NextTurn...");
        Debug.Log("CurrentTurnCharacter[" + CurrentTurn + "]: " + CharacterOrder[CurrentTurn].name);
        ChangedTurn?.Invoke();
    }

    // gets the character of the current turn
    public Character GetCurrentTurnCharacter()
    {
        Debug.Log("CurrentTurnCharacter["+CurrentTurn+"]: " + CharacterOrder[CurrentTurn].name);
        return CharacterOrder[CurrentTurn];
    }
}
