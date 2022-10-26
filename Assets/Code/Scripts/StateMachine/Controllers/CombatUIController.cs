using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUIController : MonoBehaviour
{
    [SerializeField] Text _enemyThinkingTextUI = null;

    private void OnEnable()
    {
        EnemyTurnCombatState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnCombatState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnCombatState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnCombatState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void Start()
    {
        // make sure text is disabled on start
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }

    void OnEnemyTurnBegan()
    {
        _enemyThinkingTextUI.gameObject.SetActive(true);
    }

    void OnEnemyTurnEnded()
    {
        _enemyThinkingTextUI.gameObject.SetActive(false);
    }
}
