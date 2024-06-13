using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] int maxAmmoCount;
    [SerializeField] float shootRate;
    [SerializeField] float reloadTime;
    [SerializeField] float grenadeThrowForce;

    

    int currentAmmoCount;
    bool isShooting;
    bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmoCount = maxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
            

        if (Input.GetButtonDown("Fire1") && !isShooting && currentAmmoCount > 0)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Fire3") && !isShooting)
        {
            ThrowGrenade();
        }

        if (currentAmmoCount <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, transform.rotation);
        currentAmmoCount--;

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, shootPos.position, shootPos.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootPos.forward * grenadeThrowForce, ForceMode.VelocityChange);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Quaternion rot;
        Vector3 pos;
        gun.transform.GetLocalPositionAndRotation(out pos, out rot);
        gun.transform.Rotate(new Vector3(300, 0, 0));
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = maxAmmoCount;
        gun.transform.Rotate(new Vector3(0, 0, 0));
        gun.transform.localPosition = pos;
        gun.transform.localRotation = rot;
        isReloading = false;
    }
}