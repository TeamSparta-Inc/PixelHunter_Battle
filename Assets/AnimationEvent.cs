using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] PlayerControler playerControler;
    [SerializeField] Player player;

    public void AttackEvent()
    {
        Debug.Log("한번 본다잉");
        playerControler.AttackEvent();
    }

    public void StopAnimationEvent()
    {
        player.StopAnimation(Strings.ANIMATION_MELEEATTACK);
    }
}
