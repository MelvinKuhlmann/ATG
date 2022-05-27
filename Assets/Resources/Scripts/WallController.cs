using UnityEngine;

namespace Resources.Scripts
{
    public class WallController : Obstacle
    {
        public Transform healthbar;
        
        protected override void ChildStart() { }

        protected override void ChildUpdate() { }

        protected override int InitializeDurability()
        {
            return 100;
        }

        protected override Transform GetHealthBar()
        {
            return healthbar;
        }
    }
}