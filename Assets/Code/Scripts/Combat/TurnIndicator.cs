using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// visually indicates which character's turn it is
public class TurnIndicator : MonoBehaviour
{
    [Header("Turn Indicator")]
    //[SerializeField] private int _yOffset = -6;
    [SerializeField] private Vector3 _positionOffset = new Vector3(0, -6, 0);

    [HideInInspector] public TurnController TurnController;

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
        Vector3 characterPos = character.Party.Positions[character.PartyPosition];
        transform.position = characterPos + _positionOffset;
    }
}
