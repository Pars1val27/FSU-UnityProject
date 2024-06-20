using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] PlayerClass Gunner;
    [SerializeField] Transform GrenadePos;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;
    [SerializeField] int currAmmo;
    [SerializeField] int maxAmmo;
    [SerializeField] float shootRate;
    [SerializeField] float reloadTime;
    [SerializeField] float grenadeThrowForce;
    
    bool isShooting;
    bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        currAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
            

        if (Input.GetButtonDown("Fire1") && !isShooting && currAmmo > 0)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Fire3") && !isShooting)
        {
            ThrowGrenade();
        }

        if (currAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist))
        {

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(shootDamage);
            }

        }


        currAmmo--;

        yield return new WaitForSeconds(shootRate);
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
        yield return new WaitForSeconds(reloadTime);
        currAmmo = maxAmmo;
        gun.transform.Rotate(new Vector3(0, 0, 0));
        gun.transform.localPosition = pos;
        gun.transform.localRotation = rot;
        isReloading = false;
    }
}