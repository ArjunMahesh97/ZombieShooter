using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] Camera FPcamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;

    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        canShoot = true;
    }

   //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            muzzleFlash.Play();

            ammoSlot.ReduceCurrentAmmo(ammoType);

            ProcessShot();
        }
        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    private void ProcessShot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1f);       
    }
}
