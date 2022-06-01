using UnityEngine;

namespace Resources.Scripts
{
    public class WallController : Obstacle
    {
        public GameObject fullWall;
        public GameObject brokenWall;

        protected override void ChildStart()
        {
        }

        protected override void ChildUpdate()
        {
            if (Durability <= 50)
            {
                fullWall.SetActive(false);
                brokenWall.SetActive(true);
            }
        }

        protected override int InitializeDurability()
        {
            return 100;
        }

        protected override int InitializeStrategicValue()
        {
            return 2;
        }
    }
}