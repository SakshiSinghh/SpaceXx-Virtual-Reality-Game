using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shieldController : MonoBehaviour
{
    public InputActionProperty RightHand;
    public GameObject Shield;

    private bool isShieldActive = false;
    private float shieldDuration = 2f;
    private float shieldTimer = 0f;

    private void Update()
    {
        if (RightHand.action.WasPressedThisFrame() && !isShieldActive)
        {
            ActivateShield();
        }

        if (isShieldActive)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                DeactivateShield();
            }
        }
    }

    private void ActivateShield()
    {
        isShieldActive = true;
        Shield.SetActive(true);
        shieldTimer = shieldDuration;
    }

    private void DeactivateShield()
    {
        isShieldActive = false;
        Shield.SetActive(false);
    }
}
