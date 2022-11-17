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
            //Debug.Log(i + ": " + characters[i].name);
            _addCharacterIndex++;
        }
    }

    // sets the turn order of the added characters
    public void InitializeTurns()
    {
        // create a reference of the original character list
        Character[] characters = _characterOrder;
        Array.Copy(_characterOrder, characters, _characterOrder.Length);

        // Loop through the entire array once
        for(int i = 0; i < _characterOrder.Length; i++)
        {
            // Compare each element in the array to all other elements
            for (int j = i+1; j < _characterOrder.Length; j++)
            {
                if (_characterOrder[i].BaseSpeed < _characterOrder[j].BaseSpeed)
                {
                    Character temp = _characterOrder[i];
                    _characterOrder[i] = _characterOrder[j];
                    _characterOrder[j] = temp;
                }
            }


            //Debug.Log("_charactersTurnOrder[" + i + "] = " + _characterOrder[i].name);
        }

        ChangedTurn?.Invoke();

        // Set initial turn index
        _currentTurn = 0;
        _isDisplaying = true;
        //Debug.Log("TurnController... InitializeTurns");
        //Debug.Log("TurnController... CurrentTurn: " + CurrentTurn);
        //Debug.Log("TurnController... CurrentTurnCharacter[" + CurrentTurn + "]: " + CharacterOrder[CurrentTurn].gameObject);
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
        if(_currentTurn >= CharacterOrder.Length)
        {
            _currentTurn = 0;
        }
        //Debug.Log("TurnController... NextTurn...");
        //Debug.Log("TurnController... CurrentTurn: " + CurrentTurn);
        //Debug.Log("TurnController... CurrentTurnCharacter[" + CurrentTurn + "]: " + CharacterOrder[CurrentTurn].gameObject);
        Character character = _characterOrder[_currentTurn];
        //Debug.Log("Party Position: " + character.Party.Positions[character.PartyPosition]);
        ChangedTurn?.Invoke();
    }

    // gets the character of the current turn
    public Character GetCurrentTurnCharacter()
    {
        Debug.Log("TurnController... CurrentTurnCharacter[" + CurrentTurn+"]: " + CharacterOrder[CurrentTurn].gameObject);
        return CharacterOrder[CurrentTurn];
    }
}
