using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerClass))]
public class PlayerClassEditor : Editor
{
    SerializedProperty classWeapon;
    SerializedProperty playerHP;
    SerializedProperty speed;
    SerializedProperty sprintMod;
    SerializedProperty dashCD;
    SerializedProperty dashMod;
    SerializedProperty jumpMax;
    SerializedProperty jumpSpeed;
    SerializedProperty damage;
    SerializedProperty attackSpeed;

    SerializedProperty showGunnerAttributes;
    SerializedProperty maxAmmo;
    SerializedProperty currAmmo;
    SerializedProperty shootRate;
    SerializedProperty reloadTime;
    SerializedProperty delay;
    SerializedProperty grenadeThrowForce;
    SerializedProperty explosionRadius;
    SerializedProperty explosionForce;
    SerializedProperty explosionDamage;
    SerializedProperty grenadeRechargeRate;

    void OnEnable()
    {
        classWeapon = serializedObject.FindProperty("classWeapon");
        playerHP = serializedObject.FindProperty("playerHP");
        speed = serializedObject.FindProperty("speed");
        sprintMod = serializedObject.FindProperty("sprintMod");
        dashCD = serializedObject.FindProperty("dashCD");
        dashMod = serializedObject.FindProperty("dashMod");
        jumpMax = serializedObject.FindProperty("jumpMax");
        jumpSpeed = serializedObject.FindProperty("jumpSpeed");
        damage = serializedObject.FindProperty("damage");
        attackSpeed = serializedObject.FindProperty("attackSpeed");

        showGunnerAttributes = serializedObject.FindProperty("showGunnerAttributes");
        maxAmmo = serializedObject.FindProperty("maxAmmo");
        currAmmo = serializedObject.FindProperty("currAmmo");
        shootRate = serializedObject.FindProperty("shootRate");
        reloadTime = serializedObject.FindProperty("reloadTime");
        delay = serializedObject.FindProperty("delay");
        grenadeThrowForce = serializedObject.FindProperty("grenadeThrowForce");
        explosionRadius = serializedObject.FindProperty("explosionRadius");
        explosionForce = serializedObject.FindProperty("explosionForce");
        explosionDamage = serializedObject.FindProperty("explosionDamage");
        grenadeRechargeRate = serializedObject.FindProperty("grenadeRechargeRate");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the class weapon field
        EditorGUILayout.PropertyField(classWeapon);

        // Draw the base attributes
        EditorGUILayout.PropertyField(playerHP);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(sprintMod);
        EditorGUILayout.PropertyField(dashCD);
        EditorGUILayout.PropertyField(dashMod);
        EditorGUILayout.PropertyField(jumpMax);
        EditorGUILayout.PropertyField(jumpSpeed);
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(attackSpeed);

        // Always show the toggle for gunner attributes
        EditorGUILayout.PropertyField(showGunnerAttributes);

        // Conditionally show the gunner attributes based on the toggle
        if (showGunnerAttributes.boolValue)
        {
            EditorGUILayout.PropertyField(maxAmmo);
            EditorGUILayout.PropertyField(currAmmo);
            EditorGUILayout.PropertyField(shootRate);
            EditorGUILayout.PropertyField(reloadTime);
            EditorGUILayout.PropertyField(delay);
            EditorGUILayout.PropertyField(grenadeThrowForce);
            EditorGUILayout.PropertyField(explosionRadius);
            EditorGUILayout.PropertyField(explosionForce);
            EditorGUILayout.PropertyField(explosionDamage);
            EditorGUILayout.PropertyField(grenadeRechargeRate);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
