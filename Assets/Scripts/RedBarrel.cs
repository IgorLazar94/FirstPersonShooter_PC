using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Spider;
using Enemy.Zombie;
using Player;
using UnityEngine;

public class RedBarrel : MonoBehaviour
{
    public bool IsExplosiveFromOtherBarrel { private get; set; }
    [SerializeField] private ParticleSystem fireFx, explosiveFx;
    [SerializeField] private AudioClip burningFireSound, explosiveSound;
    private MeshRenderer barrelMeshRenderer;
    private AudioSource barrelAudioSource;
    private bool isBurningBarrel = false;
    private float attackRadius = 6f;
    private int attackValue = 15;
    private float barrelBurningTime = 10f;

    private void Start()
    {
        IsExplosiveFromOtherBarrel = false;
        barrelMeshRenderer = GetComponent<MeshRenderer>();
        barrelAudioSource = GetComponent<AudioSource>();
    }

    public void BarrelTakeDamage()
    {
        CheckExplosive();
        barrelAudioSource.clip = burningFireSound;
        barrelAudioSource.Play();
        StartCoroutine(BarrelExplosionWithDelay(barrelBurningTime));
        isBurningBarrel = true;
        fireFx.Play();
    }

    private void CheckExplosive()
    {
        if (isBurningBarrel && !IsExplosiveFromOtherBarrel)
        {
            StartCoroutine(BarrelExplosionWithDelay(0f));
        }
    }

    private IEnumerator BarrelExplosionWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        barrelAudioSource.clip = explosiveSound;
        barrelAudioSource.loop = false;
        barrelAudioSource.Play();
        explosiveFx.Play();
        explosiveFx.transform.parent = null;
        CheckUnitsNearby();
        DisableBarrelMesh();
        Invoke(nameof(DestroyBarrel), 1f);
    }

    private void DisableBarrelMesh()
    {
        barrelMeshRenderer.enabled = false;
    }

    private void DestroyBarrel()
    {
        Destroy(gameObject);
    }

    private void CheckUnitsNearby()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (var findCollider in colliders)
        {
            if (findCollider.TryGetComponent(out ZombieBehaviour zombie))
            {
                zombie.TakeDamage(attackValue);
            }
            else if (findCollider.TryGetComponent(out Spider spider))
            {
                spider.SpiderDie();
            }
            else if (findCollider.TryGetComponent(out PlayerState player))
            {
                player.PlayerTakeDamage(attackValue);
            }
            else if (findCollider.TryGetComponent(out RedBarrel barrel) && barrel != this)
            {
                barrel.BarrelTakeDamage();
                barrel.IsExplosiveFromOtherBarrel = true;
            }
        }
    }
}