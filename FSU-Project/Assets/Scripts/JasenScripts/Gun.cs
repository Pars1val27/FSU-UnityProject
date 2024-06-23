using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public PlayerClass Gunner;
    [SerializeField] Transform GrenadePos;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float grenadeThrowForce;

    bool isShooting;
    bool isReloading;

    void Start()
    {
        Gunner.currAmmo = Gunner.maxAmmo;
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
           StartCoroutine(Shoot());


        }
        if (Input.GetButtonDown("Fire3") && !isShooting)
        {
            ThrowGrenade();
        }

        if (Gunner.currAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    

    IEnumerator Shoot()
    {
        isShooting = true;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Gunner.damage))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(Gunner.damage);
            }
        }

        Gunner.currAmmo--;

        yield return new WaitForSeconds(Gunner.shootRate);
        isShooting = false;
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, GrenadePos.position, GrenadePos.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(GrenadePos.forward * grenadeThrowForce, ForceMode.VelocityChange);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Quaternion rot;
        Vector3 pos;
        gun.transform.GetLocalPositionAndRotation(out pos, out rot);
        gun.transform.Rotate(new Vector3(300, 0, 0));
        yield return new WaitForSeconds(Gunner.reloadTime);
        Gunner.currAmmo = Gunner.maxAmmo;
        gun.transform.Rotate(new Vector3(0, 0, 0));
        gun.transform.localPosition = pos;
        gun.transform.localRotation = rot;
        isReloading = false;
    }
    IEnumerator flashMuzzle()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
}