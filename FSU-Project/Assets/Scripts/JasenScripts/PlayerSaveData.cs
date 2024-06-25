using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerSaveData
{
    public int playerHP;
    public int speed;
    public int sprintMod;
    public float dashCD;
    public int dashMod;
    public int jumpMax;
    public int jumpSpeed;
    public int damage;
    public int attackSpeed;
    public int maxAmmo;
    public int currAmmo;
    public float shootRate;
    public float reloadTime;
    public int delay;
    public float grenadeThrowForce;
    public float explosionRadius;
    public float explosionForce;
    public int explosionDamage;
    public float grenadeRechargeRate;
}