using UnityEngine;
using System.Collections.Generic;

public abstract class AStateSO : ScriptableObject
{
    public AConditionSO startCondition;
    public List<AConditionSO> endConditions;
    public abstract void OnStateEnter(EnemyStateMachine enemy);
    public abstract void OnStateUpdate(EnemyStateMachine enemy);
    public abstract void OnStateExit(EnemyStateMachine enemy);
}
