using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public partial class Unit : MonoBehaviour
{
    public bool run;
    public bool isMagicZone;
    public bool isDie;
    public bool canDamage;
    public float currentHealth;
    public float moveSpeed = 1;
    public UnitState state = UnitState.Idle;
    public Animator anim;
    public GameObject firePoint;
    public Unit target;
    public UnityEvent dieUnit = new UnityEvent();
    public event Action<Unit> damaged;
    public Vector3 toTarget;
    public Vector3 unitCenter;

    [SerializeField] public ParticleSystem psDeath;

    private bool notMove;
    private bool noControl;
    private bool isMove;
    private UnitState attackType;
    private Vector3 movement = Vector3.zero;
    private Rigidbody[] dollBodys;
    private NavMeshAgent agent;
    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        spells.Add(new BombSpell());
        spells.Add(new Dispell());
        spells.Add(new ElectroSpell());
        spells.Add(new FireBallSpell());
        spells.Add(new FireWallSpell());
        spells.Add(new MagicShieldSpell());
        spells.Add(new MagicTeleportSpell());
        spells.Add(new PhobiaBallSpell());
        spells.Add(new RainSpell());
        spells.Add(new SwordSpell());
    }

    private void FixedUpdate()
    {
        unitCenter = transform.position + Vector3.up;
        toTarget = target.transform.position - transform.position;
        ReplenishmentMana();
        MagicZone();
        currentSpellTime -= Time.fixedDeltaTime;
        ApplyEffects();
        StepMove();
        notMove = false;
        noControl = false;
        isMove = false;
        movement = Vector3.zero;
    }



    public void ChangeState(UnitState newState)
    {
        if ( (state == newState && newState != UnitState.Hurt) || state == UnitState.Die)
        {
            return;
        }
        UnitState oldState = state;
        state = newState;
        switch (newState)
        {
            case UnitState.Idle:
                anim.SetBool("Run", false);
                anim.SetBool("Idle", true);
                break;
            case UnitState.Run:
                anim.SetBool("Attack", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
                break;
            case UnitState.Hurt:
                anim.SetTrigger("Hurt");
                break;
            case UnitState.Die:
                anim.SetBool("Run", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Attack", false);
                anim.SetBool("Die", true);
                break;
            case UnitState.Victory:
                run = false;
                anim.SetBool("Run", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Attack", false);
                anim.SetBool("AttackSlash", false);
                anim.SetBool("AttackSpin", false);
                anim.SetBool("AttackSlash2", false);
                anim.SetTrigger("Victory");
                break;
            case UnitState.AttackSlash:
                anim.SetBool("Attack", true);
                anim.SetTrigger("AttackSlash");
                break;
            case UnitState.Spell:
                anim.SetTrigger("Spell");
                break;
            default:
                break;
        }
    }

    public Vector3 GetMovement()
    {
        return movement;
    }

    public void Attack(UnitState attack)
    {
        attackType = (UnitState)Random.Range((int)UnitState.AttackSlash, (int)UnitState.AttackSlash2 + 1);
        ChangeState(attackType);
    }

    public void FinishAttack()
    {

    }

    public void Stop()
    {
        ChangeState(UnitState.Idle);
    }

    public void Run()
    {
        ChangeState(UnitState.Run);
    }

    public void Move(Vector3 p_movement)
    {
        movement += p_movement;
    }

    public void TakeDamage(Damage damage)
    {
        currentHealth -= damage.damageValue;
        ChangeState(UnitState.Hurt);
        AddEffects(damage.effects);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDie(damage);
        }
        damaged?.Invoke(this);
    }

    public void ApplyEffects()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Apply();
        }
    }

    private void StepMove()
    {
        if (notMove || noControl)
        {
            return;
        }

        movement.y = 0;
        agent.isStopped = false;
        if (movement != Vector3.zero)  isMove = true;
        float singleStep = moveSpeed * Time.deltaTime * 5;
        Vector3 rotateDirection;
        if (movement.sqrMagnitude > 0.0001) rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);
        else rotateDirection = Vector3.RotateTowards(transform.forward, toTarget, singleStep, 0.0f);
        rotateDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(rotateDirection);
        Vector3 stepMove = movement.normalized * moveSpeed;
        stepMove.y = 0;
        agent.destination = transform.position + stepMove;
        if (isMove) ChangeState(UnitState.Run);
        else ChangeState(UnitState.Idle);

    }

}
