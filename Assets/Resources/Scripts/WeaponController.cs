using UnityEngine;

namespace Resources.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject bullet;
        public Transform firePoint;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, firePoint.position, transform.rotation);
            }
        }
    }
}
