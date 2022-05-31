using UnityEngine;

namespace Resources.Scripts
{
    public class WallController : Obstacle
    {
        protected override void ChildStart() { }

        protected override void ChildUpdate() { }

        protected override int InitializeDurability()
        {
            return 100;
        }
    }
}