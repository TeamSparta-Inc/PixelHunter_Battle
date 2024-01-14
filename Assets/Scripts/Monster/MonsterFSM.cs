using System.Collections;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    StateMachine<Enums.StateEnum> FSM;

    private void Awake()
    {
        FSM = new StateMachine<Enums.StateEnum>(this);

        FSM.ChangeState(Enums.StateEnum.Spawn);
    }

    private void Update()
    {
        FSM.Driver.Update?.Invoke();
    }

    void Spawn_Enter()
    {
        Debug.Log("Spawn");
        FSM.ChangeState(Enums.StateEnum.Idle);
    }

    void Idle_Enter()
    {

        Debug.Log("Idle");
        FSM.ChangeState(Enums.StateEnum.Run);
    }

    void Run_Enter()
    {
        Debug.Log("Run");
        FSM.ChangeState(Enums.StateEnum.MeleeAttack);
    }

    void MeleeAttack_Enter()
    {
        Debug.Log("MeleeAttack");
    }

    void RangedAttack_Enter()
    {
        Debug.Log("RangedAttack");
    }

    void Death_Enter()
    {
        Debug.Log("Death");
    }
}
