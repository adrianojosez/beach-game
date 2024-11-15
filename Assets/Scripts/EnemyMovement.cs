﻿using System;
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
        private Bounds _cameraBounds;
        private SpriteRenderer _renderer;

        public float speed = 3f;
        private Vector3 targetPosition;

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            var height = Camera.main.orthographicSize * 2f;
            var width = height * Camera.main.aspect;
            _cameraBounds = new Bounds(Vector3.zero, new Vector3(width, height));

            SetNewTargetPosition();
        }

        void Update()
        {
            MoveTowardsTarget();
        }

        void SetNewTargetPosition()
        {
            var spriteWidth = _renderer.sprite.bounds.extents.x;
            var spriteHeight = _renderer.sprite.bounds.extents.y;

            var newPositon = Vector3.zero;
            newPositon.x = Mathf.Clamp(Random.Range(-5f, 5f), _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
            newPositon.y = Mathf.Clamp(Random.Range(-5f, 5f), _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);
            
            targetPosition = newPositon;
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