using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerClass", menuName = "Player Class")]
public class PlayerClass : ScriptableObject
{
    //Any updates to the playerClass needs to be implemented in PlayerClassEditor.cs aswell

    [SerializeField] public GameObject classWeapon;

    [Header("Base Attributes")]
    [Range(1, 100)]
    [SerializeField] public int origHP;
    public int playerHP;

    [Range(1, 20)]
    [SerializeField] public int speed;

    [Range(1, 5)]
    [SerializeField] public int sprintMod;

    [Range(1, 10)]
    [SerializeField] public float dashCD;

    [Range(1, 10)]
    [SerializeField] public int dashMod;

    [Range(1, 3)]
    [SerializeField] public int jumpMax;

    [Range(1, 20)]
    [SerializeField] public int jumpSpeed;

    [Range(1, 50)]
    [SerializeField] public int damage;

    [Range(1, 10)]
    [SerializeField] public int attackSpeed;

    [Header("Gunner Attributes")]
    [SerializeField] public bool showGunnerAttributes;

    [Range(1, 99)]
    [SerializeField] public int maxAmmo;
    public int currAmmo;

    [Range(1, 100)]
    [SerializeField] public float shootDist = 100;

    [Range(0.1f, 10f)]
    [SerializeField] public float shootRate;

    [Range(0.5f, 5f)]
    [SerializeField] public float reloadTime;

    [Range(1, 5)]
    [SerializeField] public int delay;

    [Range(3, 25)]
    [SerializeField] public float grenadeThrowForce;

    [Range(1f, 20f)]
    [SerializeField] public float explosionRadius = 5f;

    [Range(10f, 100f)]
    [SerializeField] public float explosionForce = 50f;

    [Range(1, 100)]
    [SerializeField] public int explosionDamage = 5;

    [Range(0.1f, 60f)]
    [SerializeField] public float grenadeRechargeRate = 5f;


    [Header("Melee Attributes")]
    [SerializeField] public bool showMeleeAttributes;

    [Range(1, 100)]
    [SerializeField] public int meleeBlock;
    [Range(0.1f, 10f)]
    [SerializeField] public float meleeAttackRate;
    [Range(1f, 10f)]
    [SerializeField] public float meleeRange;

    public void Save()
    {
        PlayerPrefs.SetInt("PlayerHP", origHP);
        PlayerPrefs.SetInt("Speed", speed);
        PlayerPrefs.SetInt("SprintMod", sprintMod);
        PlayerPrefs.SetFloat("DashCD", dashCD);
        PlayerPrefs.SetInt("DashMod", dashMod);
        PlayerPrefs.SetInt("JumpMax", jumpMax);
        PlayerPrefs.SetInt("JumpSpeed", jumpSpeed);
        PlayerPrefs.SetInt("Damage", damage);
        PlayerPrefs.SetInt("AttackSpeed", attackSpeed);

        PlayerPrefs.SetInt("MaxAmmo", maxAmmo);
        PlayerPrefs.SetFloat("ShootRate", shootRate);
        PlayerPrefs.SetFloat("ReloadTime", reloadTime);
        PlayerPrefs.SetInt("Delay", delay);
        PlayerPrefs.SetFloat("GrenadeThrowForce", grenadeThrowForce);
        PlayerPrefs.SetFloat("ExplosionRadius", explosionRadius);
        PlayerPrefs.SetFloat("ExplosionForce", explosionForce);
        PlayerPrefs.SetInt("ExplosionDamage", explosionDamage);
        PlayerPrefs.SetFloat("GrenadeRechargeRate", grenadeRechargeRate);

        PlayerPrefs.SetInt("MeleeBlock", meleeBlock);
        PlayerPrefs.SetFloat("MeleeAttackRate", meleeAttackRate);
        PlayerPrefs.SetFloat("MeleeRange", meleeRange);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        playerHP = PlayerPrefs.GetInt("PlayerHP", origHP);
        speed = PlayerPrefs.GetInt("Speed", speed);
        sprintMod = PlayerPrefs.GetInt("SprintMod", sprintMod);
        dashCD = PlayerPrefs.GetFloat("DashCD", dashCD);
        dashMod = PlayerPrefs.GetInt("DashMod", dashMod);
        jumpMax = PlayerPrefs.GetInt("JumpMax", jumpMax);
        jumpSpeed = PlayerPrefs.GetInt("JumpSpeed", jumpSpeed);
        damage = PlayerPrefs.GetInt("Damage", damage);
        attackSpeed = PlayerPrefs.GetInt("AttackSpeed", attackSpeed);

        maxAmmo = PlayerPrefs.GetInt("MaxAmmo", maxAmmo);
        shootRate = PlayerPrefs.GetFloat("ShootRate", shootRate);
        reloadTime = PlayerPrefs.GetFloat("ReloadTime", reloadTime);
        delay = PlayerPrefs.GetInt("Delay", delay);
        grenadeThrowForce = PlayerPrefs.GetFloat("GrenadeThrowForce", grenadeThrowForce);
        explosionRadius = PlayerPrefs.GetFloat("ExplosionRadius", explosionRadius);
        explosionForce = PlayerPrefs.GetFloat("ExplosionForce", explosionForce);
        explosionDamage = PlayerPrefs.GetInt("ExplosionDamage", explosionDamage);
        grenadeRechargeRate = PlayerPrefs.GetFloat("GrenadeRechargeRate", grenadeRechargeRate);

        meleeBlock = PlayerPrefs.GetInt("MeleeBlock", meleeBlock);
        meleeAttackRate = PlayerPrefs.GetFloat("MeleeAttackRate", meleeAttackRate);
        meleeRange = PlayerPrefs.GetFloat("MeleeRange", meleeRange);
    }

    public void SaveDefault()
    {
        if (!PlayerPrefs.HasKey("DefaultSaved"))
        {
            Save();
            PlayerPrefs.SetInt("DefaultSaved", 1);
        }
    }

    public void ResetToDefault()
    {
        PlayerPrefs.DeleteAll();
        SaveDefault();
        Load();
    }
}

