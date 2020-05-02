using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public UnitState state = UnitState.Idle;
    public bool run;
    public bool isDie;
    public float health;
    public Animator anim;
    public Collider body;
    public UnityEvent dieUnit = new UnityEvent();
    public GameObject sword;
    public GameObject doll;
    public GameObject swordCollider;
    public float moveSpeed = 1;
    public bool canDamage;
    public GameObject firePoint;
    public Unit target;


    private float gravity;
    private CharacterController ch_controller;
    private Vector3 movement = Vector3.zero;
    private Rigidbody[] dollBodys;
    private UnitState attackType;
    private bool isMove;
    private Vector3 direction;
    private float spellTime = 1f;
    private float currentSpellTime;

    private void Awake()
    {
        ch_controller = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        if(currentSpellTime > 0)
        {
            currentSpellTime -= Time.fixedDeltaTime;
        }
        if (!isMove && target)
        {
            direction = target.transform.position - transform.position;
        }
        if (isMove)
        {
            ChangeState(UnitState.Run);
        }
        else
        {
            ChangeState(UnitState.Idle);
        }
        if (run) movement = movement + transform.forward;
        GamingGravity();
        movement.y = gravity;
        StepMove();
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
            //case UnitState.AttackSlash2:
            //    anim.SetBool("Attack", true);
            //    anim.SetTrigger("AttackSlash2");
            //    break;
            //case UnitState.Attack4:
            //    anim.SetBool("Attack", true);
            //    anim.SetTrigger("Attack4");
            //    break;
            //case UnitState.Attack5:
            //    break;
            //case UnitState.Attack6:
            //    break;
            default:
                break;
        }
    }

    public Vector3 GetMovement()
    {
        return movement;
    }

    public void ApplySpell(IMagicSpell spell)
    {
        if (currentSpellTime > 0) return;

        ChangeState(UnitState.Spell);
        SpellInfo sInfo = new SpellInfo()
        {
            owner = this,
        };
        spell.Apply(sInfo);
        currentSpellTime = spellTime;
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
        ch_controller.enabled = false;
        transform.position = toPoint;
        ch_controller.enabled = true;
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
        isMove = true;
        movement += p_movement;
    }


    private void StepMove()
    {
        float singleStep = moveSpeed * Time.deltaTime * 5;
        Vector3 rotateDirection;
        //rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);
        if (movement.sqrMagnitude > 0.0001) rotateDirection = Vector3.RotateTowards(transform.forward, movement, singleStep, 0.0f);
        else rotateDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);
        rotateDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(rotateDirection);
        Vector3 stepMove = movement.normalized * Time.fixedDeltaTime * moveSpeed;
        stepMove.y = gravity;
        ch_controller.Move(stepMove);
        movement = Vector3.zero;
    }


    public void TakeDamage(Damage damage)
    {
        health -= damage.value;
        ChangeState(UnitState.Hurt);
        if (health <= 0)
        {
            health = 0;
            OnDie(damage);
        }
    }
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravity -= Time.fixedDeltaTime / 4;
        else gravity = 0;
    }

    private void OnDie(Damage damage)
    {
        if (isDie) return;
        Debug.Log("die " + name);
        isDie = true;
        run = false;
        ChangeState(UnitState.Die);
        dieUnit?.Invoke();
        //Destroy(ch_controller);
        enabled = false;

        //if (doll)
        //{
        //    //float force = 50f;
        //    //doll.transform.parent = null;
        //    //doll.gameObject.SetActive(true);
        //    for (int i = 0; i < dollBodys.Length; i++)
        //    {
        //        dollBodys[i].AddForce((Vector3.up + damage.attackDirection).normalized * force, ForceMode.Impulse);
        //    }
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        transform.GetChild(i).gameObject.SetActive(false);
        //    }
        //}

    }



}
