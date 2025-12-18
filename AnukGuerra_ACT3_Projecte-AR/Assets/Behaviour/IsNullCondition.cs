using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "isNull", story: "is [variable] NULL", category: "Variable Conditions", id: "e5fa37fc6021ae46f6b332c00ff88f8f")]
public partial class IsNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Variable;

    public override bool IsTrue()
    {
        return Variable == null;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
