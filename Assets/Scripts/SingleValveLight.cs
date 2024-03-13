using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleValveLight : MonoBehaviour
{
    [SerializeField] private Material redEmissionMaterial;
    [SerializeField] private Material greenEmissionMaterial;
    private Light singleLight;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        singleLight = GetComponentInChildren<Light>();
        SwitchLight(false);
    }

    public void SwitchLight(bool isValveOpen)
    {
        if (!isValveOpen)
        {
            meshRenderer.material = greenEmissionMaterial;
            singleLight.color = Color.green;
        }
        else
        {
            meshRenderer.material = redEmissionMaterial;
            singleLight.color = Color.red;
        }
    }
    
    
    
    
}
