using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

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
    //[SerializeField] TrailRenderer bulletTrail;

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
        abilityHandler = player.GetComponent<AbilityHandler>();
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

        if (!isShooting)
        {
            anim.speed = 1;
        }
        else
        {
            anim.speed = 1 + (1 - PlayerController.playerInstance.attackSpeed);
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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward+(new Vector3(Random.Range(-.01f,0.01f), Random.Range(-.01f, 0.01f), Random.Range(-.01f, 0.01f))), out hit, PlayerController.playerInstance.shootDist))
        {

            //TrailRenderer trail = Instantiate(bulletTrail, GrenadePos.position, Quaternion.identity);

            anim.SetTrigger("Shoot");

            IDamage dmg = hit.collider.GetComponent<IDamage>();
            
            
            if (hit.transform != transform && dmg != null)
            {   
                dmg.TakeDamage(PlayerController.playerInstance.damage);
                if (hit.collider != null)
                { 
                    ApplyStatusEffects(hit.collider.gameObject); 
                }
            }
            else
            {
                Destroy(Instantiate(hitEffect, hit.point, Quaternion.identity),.25f);
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
        isShooting = false;
        isReloading = true;
        anim.speed = 1;
        anim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        currAmmo = maxAmmo;
        UpdateAmmoCount();
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
    void ApplyStatusEffects(GameObject target)
    {
        if (abilityHandler.HasAbility("FireEffect"))
        {
            var fireAbility = abilityHandler.GetAbility("FireEffect");
            if (fireAbility != null)
            {
                fireAbility.Activate(target);
            }
        }
        if (abilityHandler.HasAbility("PoisonEffect"))
        {
            var poisonAbility = abilityHandler.GetAbility("PoisonEffect");
            if (poisonAbility != null)
            {
                poisonAbility.Activate(target);
            }
        }
        if (abilityHandler.HasAbility("SlowedEffect"))
        {
            var slowAbility = abilityHandler.GetAbility("SlowedEffect");
            if (slowAbility != null)
            {
                slowAbility.Activate(target);
            }
        }
        
    }

}