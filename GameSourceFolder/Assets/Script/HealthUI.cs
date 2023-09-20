//Name : Kanchana , Sakshi
//Admin No: 2200998, 2228479
/* Description:
 * This is the health UI can be referred by the player at any time using using Left primary button
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthUI : MonoBehaviour
{
    public InputActionProperty Leftpri;
    public GameObject HealthPic;

    public bool isHealthPicActive = false;
    private float healthPicDuration = 2f;
    private float healthPicTimer = 0f;

    private void Update()
    {
        if (Leftpri.action.WasPressedThisFrame())
        {
            print("Press");
            ActivateUI();
        }

        if (isHealthPicActive)
        {
            healthPicTimer -= Time.deltaTime;
            if (healthPicTimer <= 0)
            {
                DeactivateUI();
            }
        }
    }

    private void ActivateUI()
    {
        isHealthPicActive = true;
        HealthPic.SetActive(true);
        healthPicTimer = healthPicDuration;
    }

    private void DeactivateUI()
    {
        isHealthPicActive = false;
        HealthPic.SetActive(false);
    }
}
