using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] int maxAmmoCount;
    [SerializeField] float shootRate;
    [SerializeField] float reloadTime;

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
            return;

        if (Input.GetButtonDown("Fire1") && !isShooting && currentAmmoCount > 0)
        {
            StartCoroutine(Shoot());
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

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        currentAmmoCount = maxAmmoCount;
        isReloading = false;
    }
}