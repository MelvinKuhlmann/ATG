using UnityEngine;
using UnityEngine.Events;

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

        // Start is called before the first frame update
        private void Start()
        {
            _amountOfAmmo = 10;
            updateAmmo.Invoke(_amountOfAmmo);
            
            InvokeRepeating("GarbageCollectAudioSources", 0F, 5F);
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _amountOfAmmo >= 1)
            {
                PlayGunShot();
                
                muzzleFlash.SetActive(true);
                Invoke("HideFlash", 0.1F);
                Instantiate(bullet, firePoint.position, transform.rotation);
                _amountOfAmmo -= 1;
                updateAmmo.Invoke(_amountOfAmmo);
            }
        }

        private void PlayGunShot()
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = gunShotClip;
            audioSource.Play();
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
