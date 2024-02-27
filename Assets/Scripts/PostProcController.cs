using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PostProcController : MonoBehaviour
{
    private Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
    }

    public void UpdateDamageEffect(int playerHP)
    {
        float normalizedPlayerHp = Mathf.Clamp01((float)playerHP / 100f);
        float interpolatedWeight = Mathf.Lerp(1f, 0f, normalizedPlayerHp);
        volume.weight = interpolatedWeight;
    }
}
