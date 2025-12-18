using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetEnemy", story: "[Agent] detects nearest [enemy]", category: "Action", id: "05da50d104654b8276aa1201715ab727")]
public partial class GetEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<UnitBehaviour> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    protected override Status OnStart()
    {
        Enemy.Value = null;
        List<GameObject> enemyUnits = UnitsManager.Instance.units[(Agent.Value.unitTeam == team.red) ? team.blue : team.red];
        float closestDistance = Mathf.Infinity;
        foreach (var enemy in enemyUnits)
        {
            float distance = Vector3.Distance(Agent.Value.transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                Enemy.Value = enemy;
            }
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

