using UnityEngine;

public class CombatSM : StateMachine
{
    public InputController Input => _input;

    public Party HeroParty => _heroParty;
    public Party EnemyParty => _enemyParty;
    public TurnController Turn => _turn;
    public CombatUI UI => _combatUI;

    [Header("CombatSM")]
    [SerializeField] InputController _input;
    [SerializeField] Party _heroParty;
    [SerializeField] Party _enemyParty;
    [SerializeField] TurnController _turn;
    [SerializeField] CombatUI _combatUI;
    
    void Start()
    {
        // Enter Setup Combat State
        ChangeState<SetupCombatState>();
    }
}
