using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts
{
    public class ZombieSpawner : MonoBehaviour
    {
        public GameObject zombie;
        public List<float> spawnTimes = new();
        private bool _canSpawn;

        // Start is called before the first frame update
        private void Start()
        {
            _canSpawn = true;
        }

        private void FixedUpdate()
        {
            var time = Time.fixedTime;
            if (spawnTimes.Contains(Mathf.Round(time))) Spawn(10);
        }

        private void Spawn(int amount)
        {
            if (_canSpawn)
            {
                _canSpawn = false;
                
                StartCoroutine(SpawnCooldown());

                for (var i = 0; i < amount; i++)
                {
                    var exactSpawnLocation = new Vector2(
                        Random.Range(transform.position.x - 5, transform.position.x + 5),
                        Random.Range(transform.position.y - 5, transform.position.y + 5));
                    
                    Instantiate(zombie, exactSpawnLocation, transform.rotation);
                }
            }
        }

        private IEnumerator SpawnCooldown()
        {
            yield return new WaitForSeconds(1F);
            _canSpawn = true;
        }
    }
}
