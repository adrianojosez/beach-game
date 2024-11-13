﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        private Rigidbody2D rb;
        private Vector2 moveDirection;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            ProcessInputs();
        }

        void FixedUpdate()
        {
            Move();
        }

        void ProcessInputs()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
        }

        void Move()
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            float move = Input.GetAxis("Horizontal");
            transform.Translate(new Vector2(move, 0) * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Inimigo"))
            {
                Morrer();
            }
        }

        private void Morrer()
        {
            Debug.Log("O jogador morreu!");

            gameObject.SetActive(false);

        }
    }

}
}
