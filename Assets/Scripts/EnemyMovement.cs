using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public Transform[] waypoints;
        private int waypointIndex = 0;
        public GameObject explosionPrefab;

        public float speed = 3f;
        private Vector3 targetPosition;

        void Start()
        {
            SetNewTargetPosition();
        }

        void Update()
        {
            MoveTowardsTarget();
        }

        void SetNewTargetPosition()
        {
            targetPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        }

        void MoveTowardsTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetNewTargetPosition();
            }
        }

        public void Explode()
        {
            // Instanciar a explosão
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroi o inimigo
        }
    }
}