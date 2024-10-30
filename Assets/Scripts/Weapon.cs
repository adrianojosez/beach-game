using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        public int maxAmmo = 30;
        public float fireRate = 0.5f;
        public bool isBurstFire = false; // Controle de tipo de tiro
        public float reloadTime = 2.0f;

        private int currentAmmo;
        private float nextFireTime = 0f;
        private bool isReloading = false;

        void Start()
        {
            currentAmmo = maxAmmo;
        }

        void Update()
        {
            if (isReloading) return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                if (isBurstFire)
                {
                    StartCoroutine(FireBurst());
                }
                else
                {
                    FireSingleShot();
                }
                nextFireTime = Time.time + fireRate;
            }
        }

        void FireSingleShot()
        {
            currentAmmo--;
        }

        IEnumerator FireBurst()
        {
            for (int i = 0; i < 3; i++) // Tiros em rajada
            {
                if (currentAmmo <= 0) break;
                FireSingleShot();
                yield return new WaitForSeconds(fireRate / 3);
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }
    }
}
