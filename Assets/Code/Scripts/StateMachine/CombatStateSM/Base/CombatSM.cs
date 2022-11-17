using UnityEngine;

public class CombatSM : StateMachine
{
    public InputController Input => _input;

    public HeroParty HeroParty => _heroParty;
    public EnemyParty EnemyParty => _enemyParty;
    public TurnController Turn => _turn;

    [Header("CombatSM")]
    [SerializeField] InputController _input;
    [SerializeField] HeroParty _heroParty;
    [SerializeField] EnemyParty _enemyParty;
    [SerializeField] TurnController _turn;
    
    void Start()
    {
        // Enter Setup Combat State
        ChangeState<SetupCombatState>();
    }
}
