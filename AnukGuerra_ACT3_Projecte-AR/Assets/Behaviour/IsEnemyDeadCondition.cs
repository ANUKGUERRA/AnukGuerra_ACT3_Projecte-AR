using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isEnemyDead", story: "is [Enemy] Dead", category: "Conditions", id: "3165977c0458b48db36551f6bba7800b")]
public partial class IsEnemyDeadCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;

    public override bool IsTrue()
    {
        if (Enemy.Value == null) return true;
        return Enemy.Value.GetComponent<UnitBehaviour>().health <= 0;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
