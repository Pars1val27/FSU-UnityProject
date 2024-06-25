using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerClass", menuName = "Player Class")]
public class PlayerClass : ScriptableObject
{

    [SerializeField] public GameObject classWeapon;


    [Header("Base Attributes")]

    [Range(1, 100)]
    [SerializeField] public int playerHP;

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
    [Range(1, 500)]
    [SerializeField] public int maxAmmo;
    public int currAmmo;

    [Range(0.1f, 10f)]
    [SerializeField] public float shootRate;

    [Range(0.5f, 5f)]
    [SerializeField] public float reloadTime;

    [Range(1f, 20f)]
    [SerializeField] public float explosionRadius = 5f;

    [Range(10f, 100f)]
    [SerializeField] public float explosionForce = 50f;

    [Range(1, 100)]
    [SerializeField] public int explosionDamage = 5;

    [Range(0.1f, 60f)]
    [SerializeField, HideInInspector] public float grenadeRechargeRate = 5f;
}

