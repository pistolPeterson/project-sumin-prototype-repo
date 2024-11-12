using System;
using System.Collections;
using UnityEngine;


public class Shield : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ShieldVisual shieldVisual;
    private ProgressBarCircle progressBarCircle;
    private float shieldTime = 1.25f;
    private float coolDownTime = 2.5f;
    private bool shieldIsOn = false;

    private void Start()
    {
        progressBarCircle = FindObjectOfType<ProgressBarCircle>();
        input.OnShieldPressed.AddListener(EnableShield);
        playerHealth.SetInvincibility(false);
        shieldVisual.CloseShieldVisual();
    }
    //TODO: player presses key, player is invincible for x seconds 
    //if player moves, early exit 
    //on exit/early exit theres a cooldown for x seconds

    public void EnableShield()
    {
        if (shieldIsOn) return;
        StartCoroutine(UseShield());
    }

    private IEnumerator UseShield()
    {
        shieldIsOn = true;
        ActivateShieldEffects();
        yield return new WaitForSeconds(shieldTime);
        DeactivateShieldEffects();
        progressBarCircle.AnimateBarValue(coolDownTime);
        yield return new WaitForSeconds(coolDownTime);
        shieldIsOn = false;
        progressBarCircle.ResetBarValue();
    }

    private void ActivateShieldEffects()
    {
        playerHealth.SetInvincibility(true);
        shieldVisual.ShowShieldVisual();
    }

    private void DeactivateShieldEffects()
    {
        playerHealth.SetInvincibility(false);
        shieldVisual.CloseShieldVisual();
    }
}