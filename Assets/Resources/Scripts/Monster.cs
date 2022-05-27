using UnityEngine;

namespace Resources.Scripts
{
    public abstract class Monster : MonoBehaviour
    {
        protected abstract Animator getAnimator();
        
        protected void ChangeAnimationTo(string anim)
        {
            var animator = getAnimator();
            
            foreach (var animatorControllerParameter in animator.parameters)
            {
                animator.SetBool(animatorControllerParameter.name, false);
            }
            animator.SetBool(anim, true);
        }
        
        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}