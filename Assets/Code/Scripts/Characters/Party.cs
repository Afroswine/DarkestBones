using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Party : MonoBehaviour
{
    // The characters in this party.
    public List<Character> PartyMembers => _partyMembers;
    public Vector3[] Positions => _positions;

    // Maximum party size
    public static readonly int MAXCHARACTERS = 4;
    protected const int XPOS = 24;
    protected const int YPOS = 10;
    protected const int ZPOS = 0;
    protected const int SPACING = 32;

    [Header("CharacterParty")]
    [SerializeField] List<Character> _partyMembers = new List<Character>();

    // the origin of the first character and spacing between characters thereafter
    [SerializeField] Vector3 _origin = new Vector3(XPOS, YPOS, ZPOS);
    [SerializeField] float _spacing = SPACING;

    // the positions of the party members
    private Vector3[] _positions = new Vector3[MAXCHARACTERS];

    // Cap party size
    private void OnValidate()
    {
        if(_partyMembers.Count > MAXCHARACTERS)
        {
            Debug.LogWarning("No more than " + MAXCHARACTERS + " Characters allowed in a party!");
            _partyMembers.Resize(MAXCHARACTERS);
        }
    }

    private void Awake()
    {
        // Set the possible positions
        for (int i = 0; i < _positions.Length; i++)
        {
            _positions[i] = _origin + (new Vector3(_spacing, 0, 0) * i);
        }
    }

    // swap party positions between two characters
    public void SwapPartyPositions(Character character1, Character character2)
    {
        int position1 = character1.PartyPosition;
        character1.Reposition(character2.PartyPosition);
        character2.Reposition(position1);
        UpdatePartyPositions();
    }

    public List<Character> GetPartyMembers()
    {
        return _partyMembers;
    }

    // Create the party member game
    public void InstantiateParty()
    {
        for(int i = 0; i < PartyMembers.Count; i++)
        {
            GameObject gameObject = PartyMembers[i].gameObject;
            Instantiate(gameObject, _positions[i], Quaternion.identity);
            Character character = gameObject.GetComponent<Character>();
            character.Party = this;
            character.Reposition(i);

            // TODO - shouldn't need to call ResetStats from here, but it's the only way to set current stats before they are read atm...
            character.Stats.ResetStats();
        }
    }

    public void UpdatePartyPositions()
    {
        for(int i = 0; i < PartyMembers.Count; i++)
        {
            Character character = PartyMembers[i];
            character.gameObject.transform.position = _positions[character.PartyPosition];
        }
    }
}
