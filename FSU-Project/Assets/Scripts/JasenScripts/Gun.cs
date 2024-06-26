using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public PlayerClass Gunner;
    [SerializeField] Transform GrenadePos;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] AudioClip[] shootSound;
    [SerializeField] float shootSoundVol;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] AudioSource gunAudio;

    [SerializeField] ParticleSystem hitEffect;


    bool isShooting;

    public bool isReloading;
    public bool isGrenadeReady = true;

    void Start()
    {

        Gunner.currAmmo = Gunner.maxAmmo;
        Gunner.playerHP = Gunner.origHP;
        UpdateAmmoCount();
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

    void UpdateAmmoCount()
    {
        UIManager.instance.ammoCur.text = Gunner.currAmmo.ToString();
        UIManager.instance.ammoMax.text = Gunner.maxAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        UpdateAmmoCount();
        RaycastHit hit;
        StartCoroutine(flashMuzzle());
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Gunner.shootDist))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(Gunner.damage);
            }
            else
            {
                Instantiate(hitEffect, hit.point, Quaternion.identity);
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
            rb.AddForce(GrenadePos.forward * Gunner.grenadeThrowForce, ForceMode.VelocityChange);
        }

        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        if (grenadeScript != null)
        {
            grenadeScript.Initialize(Gunner.delay, Gunner.explosionRadius, Gunner.explosionForce, Gunner.explosionDamage);
        }

        isGrenadeReady = false;
        StartCoroutine(RechargeGrenade());
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
        if (gunAudio != null && shootSound != null)
        {
            gunAudio.PlayOneShot(shootSound[Random.Range(0, shootSound.Length)], shootSoundVol);
        }
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    IEnumerator RechargeGrenade()
    {
        yield return new WaitForSeconds(Gunner.grenadeRechargeRate);
        isGrenadeReady = true;
    }
}