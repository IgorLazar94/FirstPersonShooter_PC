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
        private ViewPipeSystem viewPipeSystem;
        private Valve[] valves;
        private int closedValves;
        private bool isAlarmActivate;

        private void Start()
        {
            isAlarmActivate = false;
            valves = GetComponentsInChildren<Valve>();
            viewPipeSystem = GetComponentInChildren<ViewPipeSystem>();
            foreach (var valve in valves)
            {
                valve.SetPipeSystem(this);
            }
        }

        public void StartFire()
        {
            isAlarmActivate = true;
            fireZone.enabled = isAlarmActivate;
            fireZone.PlayFireSound(isAlarmActivate);
            fireLight.enabled = isAlarmActivate;
            viewPipeSystem.EnableAlarm(isAlarmActivate);
            viewPipeSystem.UpdateComputerText(isAlarmActivate, valves.Length - closedValves);
            foreach (var fx in fireParticles)
            {
                fx.Play();
            }
        }

        private void RemoveFire()
        {
            isAlarmActivate = false;
            fireZone.enabled = isAlarmActivate;
            fireLight.enabled = isAlarmActivate;
            fireZone.DisableFireZoneCollider();
            fireZone.PlayFireSound(isAlarmActivate);
            viewPipeSystem.EnableAlarm(isAlarmActivate);
            viewPipeSystem.UpdateComputerText(isAlarmActivate, valves.Length - closedValves);
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
            viewPipeSystem.UpdateComputerText(isAlarmActivate, valves.Length - closedValves);
            if (closedValves >= valves.Length)
            {
                RemoveFire();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                CloseValve();
            }
        }
    }
}
