using UnityEngine;

namespace Resources.Scripts
{
    public abstract class Monster : MonoBehaviour
    {
        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}