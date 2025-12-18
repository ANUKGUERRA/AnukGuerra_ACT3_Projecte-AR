using UnityEngine;

public abstract class AConditionSO : ScriptableObject
{
    public bool answer;
    public abstract bool CheckCondition(EnemyStateMachine enemy);

}
