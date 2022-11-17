using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Party : MonoBehaviour
{
    // The characters in this party.
    public Character[] Characters => _characters;
    public Vector3[] Positions => _positions;

    // Maximum party size
    public static readonly int MAXCHARACTERS = 4;
    protected const int XPOS = 24;
    protected const int YPOS = 10;
    protected const int ZPOS = 0;
    protected const int SPACING = 32;

    [Header("CharacterParty")]
    [SerializeField] Character[] _characters = new Character[0];

    // the origin of the first character and spacing between characters thereafter
    protected Vector3 _origin = new Vector3(XPOS, YPOS, ZPOS);
    protected float _spacing = SPACING;

    // the positions of the party members
    private Vector3[] _positions = new Vector3[MAXCHARACTERS];

    // Cap party size
    private void OnValidate()
    {
        if (_characters.Length > MAXCHARACTERS)
        {
            Debug.LogWarning("No more than " + MAXCHARACTERS + " Characters allowed in a party!");
            Array.Resize(ref _characters, MAXCHARACTERS);
        }
    }

    private void Awake()
    {
        _characters = _characters.RemoveNulls();
    }

    private void Start()
    {
        // Set the possible positions
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _origin + (new Vector3(_spacing, 0, 0) * i);
        }

        InstantiateParty();
    }

    private void OnEnable()
    {
        
    }

    public void ShiftPartyPositions()
    {

    }

    // swap party positions between two characters
    public void SwapPartyPositions(Character character1, Character character2)
    {
        int position1 = character1.PartyPosition;
        character1.Reposition(character2.PartyPosition);
        character2.Reposition(position1);
        UpdatePartyPositions();
    }


    // Remove any null elements from _characters and return _characters
    public Character[] GetPartyMembers()
    {
        return _characters;
    }

    // Create the party member game
    private void InstantiateParty()
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            Characters[i].Party = this;
            Characters[i].Reposition(i);
            GameObject gameObject = Characters[i].gameObject;
            Instantiate(gameObject, _positions[i], Quaternion.identity);
        }
    }

    public void UpdatePartyPositions()
    {
        for(int i = 0; i < Characters.Length; i++)
        {
            Character character = Characters[i];
            character.gameObject.transform.position = _positions[character.PartyPosition];
        }
    }
}
