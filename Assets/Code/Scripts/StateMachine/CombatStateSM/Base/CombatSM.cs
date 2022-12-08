using UnityEngine;

public class CombatSM : StateMachine
{
    public TurnController TurnController => _turn;
    public CombatUI UI => _combatUI;
    public InputController Input => _input;
    public PartyController PartyController => _partyController;

    [Header("CombatSM")]
    [SerializeField] InputController _input;
    [SerializeField] TurnController _turn;
    [SerializeField] CombatUI _combatUI;
    [SerializeField] PartyController _partyController;
    
    void Start()
    {
        // Enter Setup Combat State
        ChangeState<SetupCombatState>();
    }
}
