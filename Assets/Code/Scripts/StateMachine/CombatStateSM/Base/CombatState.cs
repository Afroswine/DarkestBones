using UnityEngine;

[RequireComponent(typeof(CombatSM))]
public class CombatState : State
{
    protected CombatSM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<CombatSM>();
    }
}
