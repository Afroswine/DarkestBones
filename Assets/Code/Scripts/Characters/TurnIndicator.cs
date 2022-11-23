using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// visually indicates which character's turn it is
public class TurnIndicator : MonoBehaviour
{
    [Header("Turn Indicator")]
    [SerializeField] private int _yOffset = -6;

    public TurnController TurnController;

    private void OnEnable()
    {
        TurnController.ChangedTurn += Reposition;
    }

    private void OnDisable()
    {
        TurnController.ChangedTurn -= Reposition;
    }

    private void Reposition()
    { 
        Character character = TurnController.CurrentCharacter();
        Vector3 position = character.Party.Positions[character.PartyPosition];
        position.y += _yOffset;
        gameObject.transform.position = position;
    }
}
