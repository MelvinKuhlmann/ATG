using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject bullet;
        public Transform firePoint;
        private int _amountOfAmmo;
        public UnityEvent<int> updateAmmo = new();

        // Start is called before the first frame update
        private void Start()
        {
            _amountOfAmmo = 10;
            updateAmmo.Invoke(_amountOfAmmo);
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _amountOfAmmo >= 1)
            {
                Instantiate(bullet, firePoint.position, transform.rotation);
                _amountOfAmmo -= 1;
                updateAmmo.Invoke(_amountOfAmmo);
            }
        }
    }
}
