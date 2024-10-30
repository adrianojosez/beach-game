using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Explosion : MonoBehaviour
    {
        public float lifespan = 1f; // Tempo em segundos antes de a explosão sumir

        void Start()
        {
            Destroy(gameObject, lifespan); // Destrói o objeto após o tempo especificado
        }
    }
}
