using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimController : MonoBehaviour
{
    [SerializeField] private Animator pistolAnimator;
    [SerializeField] private ParticleSystem muzzleFlashPistol;

    public void Shot()
    {
        pistolAnimator.SetTrigger("Shot");
        muzzleFlashPistol.Play();
    }

    public void Reload()
    {
        pistolAnimator.SetTrigger("Reload");
    }

    public void SwitchRunState(bool isRun)
    {
        pistolAnimator.SetBool("IsRun", isRun);
    }
}
