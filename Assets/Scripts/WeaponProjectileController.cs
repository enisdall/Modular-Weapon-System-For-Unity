using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponProjectileController : MonoBehaviour
{
    public enum AttackType { Raycast, Projectile, Laser }
    public AttackType attackType;

    public Transform barrelTransform;
    public GameObject projectile;

    public void PerformShoot() 
    {
        switch (attackType)
        {
            case AttackType.Raycast:
                HitScanAttack();
                break;

            case AttackType.Projectile:
                ProjectileAttack();
                break;

            case AttackType.Laser:
                LaserAttack();
                break;
        }
    }

    void HitScanAttack() 
    {
       // Debug.Log("hitscan fired");
    }

    void ProjectileAttack() 
    {
        GameObject go = Instantiate(projectile);
        go.transform.position = barrelTransform.position;
        go.transform.forward = projectile.transform.forward;

        go.GetComponent<Rigidbody>().AddForce(barrelTransform.forward * 800);
    }

    void LaserAttack() 
    {

    }
}
