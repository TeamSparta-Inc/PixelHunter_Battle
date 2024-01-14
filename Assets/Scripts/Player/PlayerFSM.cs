using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField] Player player;
[SerializeField] PlayerControler playerControler;

    StateMachine<Enums.StateEnum> FSM;

    private void Awake()
    {
        FSM = new StateMachine<Enums.StateEnum>(this);
        player = GetComponent<Player>();
    playerControler = GetComponent<PlayerControler>();
        FSM.ChangeState(Enums.StateEnum.Spawn);
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

        void Run_Enter()
        {
        player.StartAnimation(Strings.ANIMATION_RUN);
        Debug.Log(Strings.ANIMATION_RUN);
            //FSM.ChangeState(Enums.StateEnum.MeleeAttack);
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

    void MeleeAttack_Enter()
    {
    Debug.Log(Strings.ANIMATION_MELEEATTACK);
        //FSM.ChangeState(Enums.StateEnum.RangedAttack);
    }

    void RangedAttack_Enter()
    {
        Debug.Log(Strings.ANIMATION_RANGEDATTACK);
        //FSM.ChangeState(Enums.StateEnum.Death);
    }

    void Death_Enter()
    {
        Debug.Log(Strings.ANIMATION_DEATH);
    }
}
