using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerControler playerControler;

    StateMachine<Enums.StateEnum> FSM;

    WaitForSeconds _attackDelay;
    WaitForSeconds meleeAttackDelay;
    WaitForSeconds rangedAttackDelay;

    private void Awake()
    {
        FSM = new StateMachine<Enums.StateEnum>(this);
        player = GetComponent<Player>();
        playerControler = GetComponent<PlayerControler>();
        FSM.ChangeState(Enums.StateEnum.Spawn);
    }

    private void Start()
    {
        meleeAttackDelay = new WaitForSeconds(player.GetAnimationLength(Strings.ANIMATION_MELEEATTACK));
        rangedAttackDelay = new WaitForSeconds(player.GetAnimationLength(Strings.ANIMATION_RANGEDATTACK));
    }

    private void Update()
    {
        FSM.Driver.Update?.Invoke();
    }

    private void FixedUpdate()
    {
        FSM.Driver.FixedUpdate.Invoke();
    }

    void Spawn_Enter()
    {
        FSM.ChangeState(Enums.StateEnum.Idle);
        Debug.Log(Strings.ANIMATION_SPAWN);
    }

    #region Idle
    void Idle_Enter()
    {
        player.StartAnimation(Strings.ANIMATION_IDLE);
    playerControler.FindClosestMonster();
        Debug.Log(Strings.ANIMATION_IDLE);
    }

    void Idle_FixedUpdate()
    {
        if (!playerControler.CheckClosestMonster())
        {
            playerControler.FindClosestMonster();
        }
        else FSM.ChangeState(Enums.StateEnum.Run);
    }

    void Idle_Exit()
    {
        player.StopAnimation(Strings.ANIMATION_IDLE);
    }
    #endregion

    #region Run
    void Run_Enter()
    {
    player.StartAnimation(Strings.ANIMATION_RUN);
    Debug.Log(Strings.ANIMATION_RUN);
        //FSM.ChangeState(Enums.StateEnum.MeleeAttack);
    }

    void Run_Update()
    {
        if (player.isAttacking)
        {
            if (player.GetAttack())
            {
                FSM.ChangeState(Enums.StateEnum.MeleeAttack);
            }
            else
            {
                FSM.ChangeState(Enums.StateEnum.RangedAttack);
            }
        }
    }

    void Run_FixedUpdate()
    {
        if (!playerControler.Move())
        {
            FSM.ChangeState(Enums.StateEnum.Idle);
        }
    }

    void Run_Exit()
    {
        player.StopAnimation(Strings.ANIMATION_RUN);
    }

    #endregion

    #region MeleeAttack
    private float lastAttackTime = 0f;
    private float attackDelay = 1f; // 1초 딜레이

    void MeleeAttack_Enter()
    {
        _attackDelay = meleeAttackDelay;
        Debug.Log(Strings.ANIMATION_MELEEATTACK);
    }

    void MeleeAttack_Update()
    {
        if (Time.time - lastAttackTime >= attackDelay) // 마지막 공격 시간으로부터 1초가 지났는지 확인
        {
            Attack();
            lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
            Debug.Log("나 실행 됬다잉");
        }

        if (!playerControler.CheckClosestMonster() || !player.isAttacking)
        {
            FSM.ChangeState(Enums.StateEnum.Idle);
        }
    }

    void MeleeAttack_Exit()
    {
        player.StopAnimation(Strings.ANIMATION_MELEEATTACK);
    }

    #endregion

    #region RangedAttack

    void RangedAttack_Enter()
    {
        _attackDelay = rangedAttackDelay;
        Debug.Log(Strings.ANIMATION_RANGEDATTACK);
    }

    void RangedAttack_Update()
    {
        if (!playerControler.CheckClosestMonster())
        {
            FSM.ChangeState(Enums.StateEnum.Idle);
        }
    }

    void RangedAttack_Exit()
    {

    }
    #endregion


    #region Death

    void Death_Enter()
    {
        Debug.Log(Strings.ANIMATION_DEATH);
    }

    #endregion


    void Attack()
    {
        player.StartAnimation(Strings.ANIMATION_MELEEATTACK);
    }
}
