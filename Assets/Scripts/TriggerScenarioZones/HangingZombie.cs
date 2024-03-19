using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TriggerScenarioZones
{
    public class HangingZombie : MonoBehaviour
    {
        [SerializeField] private Animation zombieHangingAnim;
        [SerializeField] private Light fridgeLight;
        [SerializeField] private SpriteRenderer[] bloodsTrails;
        [SerializeField] private AudioSource fridgeAudioSource;
        [SerializeField] private AudioSource grinderAudioSource;
        [SerializeField] private AudioSource breakBonesAudioSource;
        [SerializeField] private AudioClip zombieKickFridgeSfx, breakBonesSfx, meatGrinderSfx, openFridgeSfx;
        [SerializeField] private GameObject zombieObject;
        private ParticleSystem[] bloodGushFx;
        private List<Vector3> bloodsDefaultScale = new List<Vector3>();
        private BoxCollider triggerCollider;
        

        private void Start()
        {
            fridgeAudioSource.Play();
            triggerCollider = GetComponent<BoxCollider>();
            bloodGushFx = GetComponentsInChildren<ParticleSystem>();
            InitBloodTrail();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayFridgeEventAnim();
            triggerCollider.enabled = false;
        }

        private void PlayFridgeEventAnim()
        {
            zombieHangingAnim.Play();
            grinderAudioSource.clip = openFridgeSfx;
            grinderAudioSource.Play();
        }

        public void OnHandleActivateLight() // Animation Event
        {
            fridgeLight.enabled = true;
        }
    
        public void OnHandleDeactivateLight() // Animation Event
        {
            fridgeLight.enabled = false;
            fridgeAudioSource.Stop();
            zombieObject.SetActive(false);
        }

        public void PlayBloodParticles() // Animation Event
        {
            foreach (var fx in bloodGushFx)
            {
                fx.Play();
            }
            PlayGrinderSounds();
            StartCoroutine(RestoreBloodTrailScale());
        }

        public void OnHandleZombieKickFridge()
        {
            breakBonesAudioSource.clip = zombieKickFridgeSfx;
            breakBonesAudioSource.Play();
        }

        private void PlayGrinderSounds()
        {
            breakBonesAudioSource.clip = breakBonesSfx;
            breakBonesAudioSource.Play();
            grinderAudioSource.clip = meatGrinderSfx;
            grinderAudioSource.Play();
        }

        private void InitBloodTrail()
        {
            for (int i = 0; i < bloodsTrails.Length; i++)
            {
                bloodsDefaultScale.Add(bloodsTrails[i].transform.localScale);
                bloodsTrails[i].transform.localScale = Vector3.zero;
            }
        }
    
        private IEnumerator RestoreBloodTrailScale()
        {
            for (int i = 0; i < bloodsTrails.Length; i++)
            {
                bloodsTrails[i].transform.DOScale(bloodsDefaultScale[i], 0.5f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
