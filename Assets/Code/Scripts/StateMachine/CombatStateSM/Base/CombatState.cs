using UnityEngine;

[RequireComponent(typeof(CombatSM))]
public class CombatState : State
{
    protected CombatSM StateMachine { get; private set; }

    [SerializeField] private MainMenuSM _mainMenuStateMachine;
    protected MainMenuSM MainMenuStateMachine => _mainMenuStateMachine;

    private void Awake()
    {
        StateMachine = GetComponent<CombatSM>();
        _mainMenuStateMachine = GetComponent<MainMenuSM>();
    }
}
