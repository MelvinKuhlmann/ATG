using UnityEngine;
using UnityEngine.Events;
using static Resources.Scripts.Utility.AudioUtil;

namespace Resources.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        private int _amountOfAmmo;
        
        public GameObject bullet;
        public Transform firePoint;
        public GameObject muzzleFlash;
        public UnityEvent<int> updateAmmo = new();
        public AudioClip gunShotClip;
        public AudioClip noAmmoClip;

        // Start is called before the first frame update
        private void Start()
        {
            _amountOfAmmo = 10;
            updateAmmo.Invoke(_amountOfAmmo);
            
            InvokeRepeating("GarbageCollectAudioSources", 0F, 3F);
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_amountOfAmmo >= 1)
                {
                    PlayClip(gameObject, gunShotClip);
                
                    muzzleFlash.SetActive(true);
                    Invoke("HideFlash", 0.1F);
                    Instantiate(bullet, firePoint.position, transform.rotation);
                    _amountOfAmmo -= 1;
                    updateAmmo.Invoke(_amountOfAmmo);
                }
                else
                {
                    PlayClip(gameObject, noAmmoClip);
                }

            }
        }

        private void PlayGunShot()
        {

        }

        private void HideFlash()
        {
            muzzleFlash.SetActive(false);
        }

        private void GarbageCollectAudioSources()
        {
            foreach (var audioSource in gameObject.GetComponents<AudioSource>())
            {
                if (!audioSource.isPlaying)
                {
                    Destroy(audioSource);
                }
            }
        }
    }
}
