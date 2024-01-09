using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon", order = 0)]
public class WeaponTypes : ScriptableObject
{
    public string name;
    public bool AddBulletSpread = true;
    public Vector3 BulletSpreadVariance = new Vector3(0.05f, 0.05f, 0.05f);
    public ParticleSystem ImpactParticleSystem;
    public TrailRenderer BulletTrail;
    public float ShootDelay = 0.5f;
    public LayerMask Mask;
    public float BulletSpeed = 100;
    public int damage;
    public AudioClip shootingAudio;
    public AudioClip reloadAudio;
    public int maxAmmo;
    public int totalAmountOfAmmoRemaining;
    public int magazineSize;
    public int ammo;
}
