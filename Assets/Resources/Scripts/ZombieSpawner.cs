using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Resources.Scripts
{
    public class ZombieSpawner : MonoBehaviour
    {
        public UnityEvent zombieSpawn;
        
        private bool _canSpawn;
        public GameObject zombie;
        
        // Start is called before the first frame update
        private void Start()
        {
            _canSpawn = true;
        }

        public void Spawn(int amount)
        {
            if (_canSpawn)
            {
                zombieSpawn.Invoke();
                
                _canSpawn = false;
                
                StartCoroutine(SpawnCooldown());

                for (var i = 0; i < amount; i++)
                {
                    var exactSpawnLocation = new Vector2(
                        Random.Range(transform.position.x - 5, transform.position.x + 5),
                        Random.Range(transform.position.y - 5, transform.position.y + 5));
                    
                    Instantiate(zombie, exactSpawnLocation, transform.rotation);
                }
                
                Debug.Log($"{amount} zombies spawned.");
            }
        }

        private IEnumerator SpawnCooldown()
        {
            yield return new WaitForSeconds(1F);
            _canSpawn = true;
        }
    }
}
