using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private Dictionary<string, float> animationLengths = new Dictionary<string, float>();

    private bool isMeleeAttack = true;


        private void Awake()
        {
            InitializeAnimationLengths();
        }

        private void InitializeAnimationLengths()
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                animationLengths[clip.name] = clip.length;
            }
        }

        public float GetAnimationLength(string animationName)
        {
            if (animationLengths.TryGetValue(animationName, out float length))
            {
                return length;
            }
            else
            {
                Debug.LogWarning("Animation not found: " + animationName);
                return 0f;
            }
        }

        // 애니메이션을 키거나 끔
        public float StartAnimation(string animationName)
        {
            animator.SetBool(animationName, true);
            return GetAnimationLength(animationName);
        }

        public float StopAnimation(string animationName)
        {
            animator.SetBool(animationName, false);
            return GetAnimationLength(animationName);
        }


    public void ChangeAttack()
    {
        isMeleeAttack = !isMeleeAttack;
    }

    public bool GetAttack()
    {
        return isMeleeAttack;
    }
}
