using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public abstract class Obstacle : MonoBehaviour
    {
        private readonly HashSet<int> _monstersAttackingThisObject = new();
        private Vector3 _localScale;
        protected int Durability;
        protected bool IsAttackable;
        protected int StrategicValue;

        private void Start()
        {
            Durability = InitializeDurability();
            StrategicValue = InitializeStrategicValue();
            IsAttackable = true;
            ChildStart();
        }

        private void Update()
        {
            ChildUpdate();

            if (Durability <= 0)
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
            Durability -= 10;
        }

        public bool CanAttack()
        {
            return IsAttackable;
        }

        public int GetStrategicValue()
        {
            return StrategicValue;
        }

        private IEnumerator RemoveMonsterFromList(int monsterHashcode)
        {
            yield return new WaitForSeconds(1F);
            _monstersAttackingThisObject.Remove(monsterHashcode);
        }

        protected abstract void ChildStart();
        protected abstract void ChildUpdate();
        protected abstract int InitializeDurability();
        protected abstract int InitializeStrategicValue();
    }
}