using UnityEngine;

public static class AnimatorUtil
{
    public static void ChangeAnimationTo(Animator animator, string anim)
    {
        foreach (var animatorControllerParameter in animator.parameters)
        {
            animator.SetBool(animatorControllerParameter.name, false);
        }
        animator.SetBool(anim, true);
    }
}