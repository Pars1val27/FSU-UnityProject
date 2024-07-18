using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using System.Linq.Expressions;

public class GunScript : MonoBehaviour
{
    public AbilityHandler abilityHandler;
    [SerializeField] public GameObject player;
    [SerializeField] Transform GrenadePos;
    [SerializeField] GameObject muzzleFlash;
    [SerializeField] AudioClip[] shootSound;
    [SerializeField] float shootSoundVol;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] AudioSource gunAudio;

    [SerializeField] GameObject hitEffect;

    private Animator anim;

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
        abilityHandler = AbilityHandler.handlerInstance;
        anim = GetComponent<Animator>();
        currAmmo = maxAmmo;
        UpdateAmmoCount();
    }

    void Update()

    {
        
        if (isReloading)
        {
            return;
        }
        if (Input.GetButton("Fire1") && !isShooting && !UIManager.instance.gamePause && currAmmo > 0 && !UIManager.instance.abilityMenuOpen && !isReloading)
        {

            StartCoroutine(Shoot());

        }
        if (Input.GetButtonDown("Fire3") && isGrenadeReady)
        {
            ThrowGrenade();
        }

        if (Input.GetButtonDown("Reload") && !isReloading && currAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + (new Vector3(Random.Range(-.01f, 0.01f), Random.Range(-.01f, 0.01f), Random.Range(-.01f, 0.01f))), out hit, PlayerController.playerInstance.shootDist))
        {
            anim.SetTrigger("Shoot");
            anim.speed = anim.speed * (1 + (1 - PlayerController.playerInstance.attackSpeed));

            IDamage dmg = hit.collider.GetComponent<IDamage>();


            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(PlayerController.playerInstance.damage);

                if (hit.collider != null)
                {
                    Debug.Log("Applying status effects to: " + hit.collider.name);
                    ApplyStatusEffects(hit.collider.gameObject);
                }
                // else { Debug.Log("ApplyStatus Hit Collider null"); }
            }
            else
            {
                Destroy(Instantiate(hitEffect, hit.point, Quaternion.identity), .25f);
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
    public void ApplyStatusEffects(GameObject target)
    {
        Debug.Log(abilityHandler.abilities);
        foreach (var ability in abilityHandler.abilities)
        {
            if (ability is FireEffect fireEffect && abilityHandler.hasFireEffect)
            {
                Debug.Log("Activating FireEffect on target: " + target.name);
                abilityHandler.ApplyFireDamage(target, fireEffect);
            }
            if (ability is PoisonEffect poisonEffect && abilityHandler.hasPoisonEffect)
            {
                Debug.Log("Activating PoisonEffect on target: " + target.name);
                abilityHandler.ApplyPoisonDamage(target, poisonEffect);
            }
            if (ability is SlowedEffect slowEffect && abilityHandler.hasSlowEffect)
            {
                Debug.Log("Activating SlowEffect on target: " + target.name);
                abilityHandler.ApplySlow(target, slowEffect);
            }
            if (ability is FreezeEffect freezeEffect && abilityHandler.hasFreezeEffect)
            {
                Debug.Log("Activating FreezeEffect on target: " + target.name);
                abilityHandler.ApplyFreeze(target, freezeEffect);
            }
        }
    }
}