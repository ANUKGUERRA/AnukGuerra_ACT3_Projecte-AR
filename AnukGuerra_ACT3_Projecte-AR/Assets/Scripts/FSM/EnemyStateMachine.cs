using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        TelegraphChargeAttack,
        ChargeAttack,
        TelegraphSpinAttack,
        SpinAttack
    }

    [Header("State Machine")]
    public AStateSO currentState;
    public EnemyState state = EnemyState.Idle;
    public List<AStateSO> states;
    public Animator animator;
    public int performAttack = 0;

    [Header("Chase Settings")]
    public Transform playerTransform;
    public NavMeshAgent navMeshAgent;
    public float chaseDistance = 10f;
    public float forgetDistance = 15f;
    public float chaseSpeed = 3.5f;

    [Header("Charge Attack Settings")]
    public SphereCollider chargeAttackCollider;
    public float cooldownBetweenChargeAttacks = 5f;
    public float cooldownTimerChargeAttack = 0f;
    public float chargeAttackDamage = 25f;
    public float chargeDistance = 3.5f;
    public float chargeTime = 1f;
    public bool performingChargeAttack = false;

    [Header("Telegraph Charge Attack Settings")]
    public GameObject telegraphChargeAttackEffect;
    public GameObject telegraphChargeAttackEffectImage;
    public float telegraphChargeDuration = 2f;
    public bool finishedTelegraphingChargeAttack = false;

    [Header("Spin Attack Settings")]
    public SphereCollider spinAttackCollider;
    public float cooldownBetweenSpinAttacks = 7f;
    public float cooldownTimerSpinAttack = 0f;
    public float spinAttackDuration = 3f;
    public float spinAttackDamage = 15f;
    public float spinAttackRange = 4f;
    public bool performingSpinAttack = false;
    public bool isSpinning = false;
    public float spinTimer = 0f;
    public float spinSpeed = 1800;



    [Header("Telegraph Spin Attack Settings")]
    public GameObject telegraphSpinAttackEffect;
    public GameObject telegraphSpinAttackEffectImage;
    public float telegraphSpinDuration = 2f;
    public bool finishedTelegraphingSpinAttack = false;

    private void Start()
    {
        if (currentState != null)
        {
            currentState.OnStateEnter(this);
        }
    }
    private void Update()
    {
        currentState.OnStateUpdate(this);
        UpdateCooldowns();
        CheckEndingConditions();
    }
    void UpdateCooldowns()
    {
        if (cooldownTimerChargeAttack > 0f)
        {
            cooldownTimerChargeAttack -= Time.deltaTime;
            if (cooldownTimerChargeAttack < 0f) cooldownTimerChargeAttack = 0f;
        }
        if (cooldownTimerSpinAttack > 0f)
        {
            cooldownTimerSpinAttack -= Time.deltaTime;
            if (cooldownTimerSpinAttack< 0f) cooldownTimerSpinAttack = 0f;
        }
    }
    void CheckEndingConditions()
    {
        foreach (AConditionSO condition in currentState.endConditions)
        {
            if (condition.CheckCondition(this) == condition.answer)
            {
                ExitCurrentNode();
                break;
            }
        }
    }
    void ExitCurrentNode()
    {
        foreach (AStateSO stateSO in states)
        {
            if (stateSO.startCondition == null)
            {
                EnterState(stateSO);
                break;
            }
            else if (stateSO.startCondition.CheckCondition(this) == stateSO.startCondition.answer)
            {
                EnterState(stateSO);
                break;
            }
        }
    }
    void EnterState(AStateSO newState)
    {
        currentState.OnStateExit(this);
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}
