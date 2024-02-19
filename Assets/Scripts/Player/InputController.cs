using UnityEngine;

namespace Player
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private WeaponController weaponController;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                weaponController.Shot();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponController.Reload();
            }
        }
    }
}
