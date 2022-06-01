using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public abstract class Obstacle : MonoBehaviour
    {
        private readonly HashSet<int> _monstersAttackingThisObject = new();
        private int _durability;
        private Vector3 _localScale;

        private void Start()
        {
            _durability = InitializeDurability();
            ChildStart();
        }

        private void Update()
        {
            ChildUpdate();

            if (_durability <= 0)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var monsterHashcode = other.gameObject.GetHashCode();
            if (other.gameObject.CompareTag("Monster") && !_monstersAttackingThisObject.Contains(monsterHashcode))
            {
                _monstersAttackingThisObject.Add(monsterHashcode);

                TakeDamage();
                
                StartCoroutine(RemoveMonsterFromList(monsterHashcode));
            }
        }

        private void TakeDamage()
        {
            _durability -= 10;
        }

        private IEnumerator RemoveMonsterFromList(int monsterHashcode)
        {
            yield return new WaitForSeconds(1F);
            _monstersAttackingThisObject.Remove(monsterHashcode);
        }

        protected abstract void ChildStart();
        protected abstract void ChildUpdate();
        protected abstract int InitializeDurability();
    }
}