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

    [SerializeField] public ParticleSystem psDeath;

    private bool isMove;
    private float gravity;
    private float currentPushTime;
    private UnitState attackType;
    private Vector3 push;
    private Vector3 direction;
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
        toTarget = target.transform.position - transform.position;
        ReplenishmentMana();
        MagicZone();
        if (currentSpellTime > 0)
        {
            currentSpellTime -= Time.fixedDeltaTime;
        }
        if (!isMove && target)
        {
            direction = target.transform.position - transform.position;
        }

        if (run) movement = movement + transform.forward;
        movement.y = gravity;
        StepMove();
        if (isMove) ChangeState(UnitState.Run);
        else    ChangeState(UnitState.Idle);
        isMove = false;
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

    public void Teleport(Vector3 toPoint)
    {
        agent.enabled = false;
        transform.position = toPoint;
        agent.enabled = true;
        agent.destination = toPoint;
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


    public void Push(Vector3 direction)
    {
        direction.y = 0;
        
        push = transform.position + direction.normalized;

        currentPushTime = 0.5f;
    }

    public void TakeDamage(Damage damage)
    {
        currentHealth -= damage.damageValue;
        ChangeState(UnitState.Hurt);
        ApplyEffects(damage.effects);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDie(damage);
        }
        damaged?.Invoke(this);
    }

    private void ApplyEffects(List<IUnitEffect> effects)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].Apply(this);
        }

    }

    private void StepMove()
    {
        if(currentPushTime > 0)
        {
            agent.isStopped = false;
            currentPushTime -= Time.fixedDeltaTime;
            if(currentPushTime <= 0)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.destination = push;
                if (agent.remainingDistance <= agent.stoppingDistance * 2)
                    push = Vector3.zero;
            }
        }
        else
        {
            agent.isStopped = false;
            if (movement != Vector3.zero)  isMove = true;
            float singleStep = moveSpeed * Time.deltaTime * 5;
            Vector3 rotateDirection;
            if (movement.sqrMagnitude > 0.0001) rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);
            else rotateDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
            rotateDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(rotateDirection);
            Vector3 stepMove = movement.normalized * moveSpeed;
            stepMove.y = 0;
            agent.destination = transform.position + stepMove;

        }
        movement = Vector3.zero;
    }

    private void OnDie(Damage damage)
    {
        if (isDie) return;
        Debug.Log("die " + name);
        isDie = true;
        run = false;
        ChangeState(UnitState.Die);
        dieUnit?.Invoke();
        StartCoroutine(OnDeathPS(psDeath));
        enabled = false;
    }

    public IEnumerator OnDeathPS(ParticleSystem psDeath)
    {
        float timeDeath = 4f;
        float delay = 3f;

        yield return new WaitForSeconds(delay);
        psDeath.gameObject.SetActive(true);
        psDeath.gameObject.transform.parent = null;
        Destroy(psDeath.gameObject, timeDeath);
    }

}
