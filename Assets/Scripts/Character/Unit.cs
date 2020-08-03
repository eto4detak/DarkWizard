using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public partial class Unit : MonoBehaviour, IPunObservable, IHealth
{
    public Vector3 toTarget;
    public Vector3 unitCenter;
    public float currentHealth;
    public float moveSpeed = 0.1f;
    public float currentMana;
    public float maxMana;
    public bool run;
    public bool isMagicZone;
    public bool isDie;
    public bool canDamage;
    public bool isMove;
    public event Action<Unit> damaged;
    public UnityEvent dieUnit = new UnityEvent();
    public UnityEvent startUnit = new UnityEvent();
    public UnitState state = UnitState.Idle;
    public List<IUnitEffect> effects = new List<IUnitEffect>();
    public Animator anim;
    public GameObject firePoint;
    public GameObject target;
    public event Action<Unit> changeMana;
    public ParticleSystem psDeath;

    [SerializeField]  protected float spellTime = 0.5f;
    protected float currentSpellTime;
    protected List<AMagicSpell> spells = new List<AMagicSpell>();

    private Vector3 moveTarget;
    private bool isMoveTarget;
    private bool notMove;
    private bool noControl;
    private UnitState attackType;
    private Vector3 movement = Vector3.zero;
    private Rigidbody[] dollBodys;
    private Vector3 oldPosition;
    private CharacterController ch_control;
    private float gravitiForce;
    private float gravitiBoost;

    private void Awake()
    {
        UnitManager.instance.AddUnit(this);
        ch_control = GetComponent<CharacterController>();

        spells.Add(new BombSpell());
        spells.Add(new Dispell());
        spells.Add(new ElectroSpell());
        spells.Add(new FireBallSpell());
        spells.Add(new FireWallSpell());
        spells.Add(new MagicShieldSpell());
        spells.Add(new MagicTeleportSpell());
        //spells.Add(new PhobiaBallSpell());
        spells.Add(new RainSpell());
        spells.Add(new SwordSpell());
        //spells.Add(new FireBallBombSpell());
        spells.Add(new MagicPushSpell());
        spells.Add(new MagicWallSpell());
        //  spells.Add(new BlackHoleSpell());
    }

    private void OnEnable()
    {
        moveTarget = transform.position;
    }

    private void FixedUpdate()
    {
        float minOffset = 0.1f;
        FindTarget();
        unitCenter = transform.position + Vector3.up;

        ReplenishmentMana();
        MagicZone();
        currentSpellTime -= Time.fixedDeltaTime;
        ApplyEffects();

        if (target)
        {
            toTarget = target.transform.position - transform.position;
        }
        Vector3 moveDirect = moveTarget - transform.position;
        moveDirect.y = 0;
        if (isMoveTarget)
        {
            if (moveDirect.magnitude > minOffset)
            {
                Move(moveDirect);
            }
            else
            {
                isMoveTarget = false;
            }
        }


        StepMove();
        oldPosition = transform.position;
        
        OnMove();

        notMove = false;
        noControl = false;
        movement = Vector3.zero;
    }



    public void SetMoveTarget(Vector3 target)
    {
        isMoveTarget = true;
        moveTarget = target;
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

    public void FindTarget()
    {
        Transform tt = transform.GetClosest(ObjectManager.instance.objects);
        if (tt)
        {
            target = tt.gameObject;
        }
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
        float minDist = 0.00001f;


        if (notMove || noControl)
        {
            return;
        }
        movement.y = 0;
        float deltaMve = (oldPosition - transform.position).magnitude;

        isMove = deltaMve > minDist;

        RotateUnit();
        if (deltaMve > minDist) ChangeState(UnitState.Run);
        else ChangeState(UnitState.Idle);
    }

    private void RotateUnit()
    {
        Vector3 rotateDirection;
        float singleStep = 0.2f;

        rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);

        if (isMove)
        {
            rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);
        }
        else if(target)
        {
            rotateDirection = Vector3.RotateTowards(transform.forward, toTarget, singleStep, 0.0f);
        }
        rotateDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(rotateDirection);
    }

    private void OnMove()
    {
        movement = movement.normalized * moveSpeed;
        movement = new Vector3(movement.x, gravitiBoost, movement.z);
        GamingGravity();
        ch_control.Move(movement);
    }

    private void GamingGravity()
    {
        float kGravity = 1f;
        if (ch_control.isGrounded)
        {
            gravitiBoost = 0;
        }
        else
        {
            gravitiForce -= Time.deltaTime * kGravity;
            gravitiBoost += gravitiForce;
            if (gravitiBoost > 0.2) gravitiBoost = 0.2f;
            if (gravitiBoost < -0.2) gravitiBoost = -0.2f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (float)stream.ReceiveNext();
        }
    }
}
