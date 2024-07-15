using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class GunScript : MonoBehaviour
{
    public AbilityHandler abilityHandler;
    //[SerializeField] public GameObject gun;
    [SerializeField] Transform GrenadePos;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] AudioClip[] shootSound;
    [SerializeField] float shootSoundVol;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] AudioSource gunAudio;

    [SerializeField] ParticleSystem hitEffect;

    public int currAmmo;
    public int maxAmmo;
    [SerializeField] public float reloadTime;

    public float grenadeThrowForce;
    public float delay;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public float grenadeRechargeRate;

    bool isShooting;

    public bool isReloading;
    public bool isGrenadeReady = true;

    void Start()
    {
        currAmmo = maxAmmo;
        UpdateAmmoCount();
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (Input.GetButton("Fire1") && !isShooting && !UIManager.instance.gamePause && currAmmo > 0 && !UIManager.instance.abilityMenuOpen)
        {

            StartCoroutine(Shoot());

        }
        if (Input.GetButtonDown("Fire3") && isGrenadeReady)
        {
            ThrowGrenade();
        }

        if (currAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void UpdateAmmoCount()
    {
        UIManager.instance.ammoCur.text = currAmmo.ToString();
        UIManager.instance.ammoMax.text = maxAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        currAmmo--;
        UpdateAmmoCount();
        RaycastHit hit;
        StartCoroutine(flashMuzzle());
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward+(new Vector3(Random.Range(-.001f,0.001f), Random.Range(-.001f, 0.001f), Random.Range(-.001f, 0.001f))), out hit, PlayerController.playerInstance.shootDist))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            
            
            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(PlayerController.playerInstance.damage);
                //ApplyStatusEffects(hit.collider.gameObject);
            }
            else
            {
                //Instantiate(hitEffect, hit.point, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(PlayerController.playerInstance.attackSpeed);
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

        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        if (grenadeScript != null)
        {
            grenadeScript.Initialize(delay, explosionRadius, explosionForce, explosionDamage);
        }


        StartCoroutine(RechargeGrenade());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Quaternion rot;
        Vector3 pos;
        PlayerController.playerInstance.classWeaponInstance.transform.GetLocalPositionAndRotation(out pos, out rot);
        PlayerController.playerInstance.classWeaponInstance.transform.Rotate(new Vector3(300, 0, 0));
        yield return new WaitForSeconds(reloadTime);
        currAmmo = maxAmmo;
        UpdateAmmoCount();
        PlayerController.playerInstance.classWeaponInstance.transform.Rotate(new Vector3(0, 0, 0));
        PlayerController.playerInstance.classWeaponInstance.transform.localPosition = pos;
        PlayerController.playerInstance.classWeaponInstance.transform.localRotation = rot;
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
        isGrenadeReady = false;
        yield return new WaitForSeconds(grenadeRechargeRate);
        isGrenadeReady = true;
    }
    //void ApplyStatusEffects(GameObject target)
    //{
    //    if (abilityHandler.HasAbility("FireEffect"))
    //    {
    //        var fireAbility = abilityHandler.GetAbility("FireEffect");
    //        if (fireAbility != null)
    //        {
    //            fireAbility.Activate(target);
    //        }
    //    }
    //    if (abilityHandler.HasAbility("PoisonEffect"))
    //    {
    //        var poisonAbility = abilityHandler.GetAbility("PoisonEffect");
    //        if (poisonAbility != null)
    //        {
    //            poisonAbility.Activate(target);
    //        }
    //    }
    //    if (abilityHandler.HasAbility("SlowedEffect"))
    //    {
    //        var slowAbility = abilityHandler.GetAbility("SlowedEffect");
    //        if (slowAbility != null)
    //        {
    //            slowAbility.Activate(target);
    //        }
    //    }

    //}

}