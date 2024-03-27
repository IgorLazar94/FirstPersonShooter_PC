using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Enemy.Spider
{
    public class SpiderSpawner : MonoBehaviour
    {
        [SerializeField] private FirstPersonController player;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Spider spiderRed, spiderYellow, spiderGreen;
        [SerializeField] private AudioClip spiderMoveSound, spiderDieSound, spiderAttackSound;
        [SerializeField] private Transform spiderWebBarrier;
        private int spidersCounter = 30;
        private float spawnAmplitude = 3f;

        private void Start()
        {
            EnableBarrier(false);
        }

        public void StartSpawnSpiders()
        {
            StartCoroutine(SpawnSpidersWithInterval());
            EnableBarrier(true);
        }

        private IEnumerator SpawnSpidersWithInterval()
        {
            while (spidersCounter > 0)
            {
                yield return new WaitForSeconds(1.0f);
                SpawnNewRandomSpider();
                spidersCounter--;
            }

            EnableBarrier(false);
        }

        private void SpawnNewRandomSpider()
        {
            Vector3 spawnPoint = ChooseRandomSpawnPoint();
            Spider newSpider;
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    newSpider = Instantiate(spiderGreen, spawnPoint, Quaternion.identity);
                    newSpider.SetPlayer(player);
                    newSpider.SetAudio(spiderDieSound, spiderAttackSound, spiderMoveSound);
                    break;
                case 1:
                    newSpider = Instantiate(spiderRed, spawnPoint, Quaternion.identity);
                    newSpider.SetPlayer(player);
                    newSpider.SetAudio(spiderDieSound, spiderAttackSound, spiderMoveSound);
                    break;
                case 2:
                    newSpider = Instantiate(spiderYellow, spawnPoint, Quaternion.identity);
                    newSpider.SetPlayer(player);
                    newSpider.SetAudio(spiderDieSound, spiderAttackSound, spiderMoveSound);
                    break;
            }
        }

        private Vector3 ChooseRandomSpawnPoint()
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAmplitude, spawnAmplitude),
                0f,
                Random.Range(-spawnAmplitude, spawnAmplitude)
            );

            int randomIndex = Random.Range(0, spawnPoints.Length);
            var spawnPoint = spawnPoints[randomIndex].position;
            var finalSpawnPoint = spawnPoint + randomPosition;
            return finalSpawnPoint;
        }

        private void EnableBarrier(bool isEnable)
        {
            spiderWebBarrier.gameObject.SetActive(isEnable);
            if (isEnable)
            {
                spiderWebBarrier.localScale = Vector3.zero;
                spiderWebBarrier.DOScale(Vector3.one * 0.5f, 0.25f);
            }
            else
            {
                spiderWebBarrier.DOScale(Vector3.zero, 0.25f);
            }
        }
    }
}