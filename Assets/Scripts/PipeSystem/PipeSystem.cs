using System;
using UnityEngine;

namespace PipeSystem
{
    public class PipeSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] fireParticles;
        [SerializeField] private ParticleSystem[] afterFireParticles;
        [SerializeField] private FireZone fireZone;
        [SerializeField] private Light fireLight;
        private Valve[] valves;
        private int closedValves;

        private void Start()
        {
            valves = GetComponentsInChildren<Valve>();
            foreach (var valve in valves)
            {
                valve.SetPipeSystem(this);
            }
        }

        public void StartFire()
        {
            fireZone.enabled = true;
            fireZone.PlayFireSound(true);
            fireLight.enabled = true;
            foreach (var fx in fireParticles)
            {
                fx.Play();
            }
        }

        private void RemoveFire()
        {
            fireZone.enabled = false;
            fireLight.enabled = false;
            fireZone.DisableFireZoneCollider();
            fireZone.PlayFireSound(false);
            foreach (var fx in fireParticles)
            {
                fx.Stop();
            }
            foreach (var fx in afterFireParticles)
            {
                fx.Play();
            }
        }

        public void CloseValve()
        {
            closedValves++;
            if (closedValves >= valves.Length)
            {
                RemoveFire();
            }
        }
    }
}
