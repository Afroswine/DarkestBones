using UnityEngine;

public class CombatSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;
    
    void Start()
    {
        ChangeState<SetupCombatState>();
    }
}
