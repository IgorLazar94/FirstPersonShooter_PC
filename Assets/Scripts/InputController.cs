using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private WeaponAnimController weaponAnimController;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponAnimController.Shot();
        }
    }
}
