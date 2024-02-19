using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleGameObject : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableOverTime());
    }

    private IEnumerator DisableOverTime()
    {
        yield return new WaitForSeconds(20f);
        this.gameObject.SetActive(false);
    }
}
