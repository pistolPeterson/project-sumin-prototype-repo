using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour {
    
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerHealth hp;
    [SerializeField] private float maxCharge = 10f;
    [SerializeField] private float shieldCost = 1f;
    [SerializeField] private float durationTillRegen = 1f;
    [SerializeField] private float regenDelay = 0.5f;
    [SerializeField] private float regenRate = 1f;

    [Header("Debug")]
    [SerializeField] private float currentCharge;
    [SerializeField] private float timeWithoutUse = 0f;
    [SerializeField] private bool usingShield = false;
    [SerializeField] private bool canUse = true;

    [HideInInspector] public UnityEvent<float> OnShieldUse; // to update UI

    private void Start() {
        input.OnShieldUse.AddListener(UseShield);
        input.OnShieldHeld.AddListener(IsUsingShield);
        currentCharge = maxCharge;
        timeWithoutUse = 0f;
    }
    /*
     * Logic:
     * - press or hold to use
     * - when release, start timer that counts to the time
     * - when it reaches the time, regenerate health
     * - during regen, cannot use until full
     */
    private void Update() {
        if ((usingShield && currentCharge != 0) || ChargeAvailable()) {
            timeWithoutUse = 0f;
            return;
        }
        hp.SetInvincibility(false);
        timeWithoutUse += Time.deltaTime;
        if (timeWithoutUse >= durationTillRegen) {
            if (canUse)
            StartCoroutine(RegenerateShield());
        }
    }
    public IEnumerator RegenerateShield() {
        canUse = false;
        while (currentCharge < maxCharge && !canUse) {
            AddCharge(regenRate);
            OnShieldUse.Invoke(currentCharge);
            yield return new WaitForSeconds(regenDelay);
        }
    }
    public bool ChargeAvailable() {
        return currentCharge >= shieldCost;
    }
    public void IsUsingShield(bool shielding) {
        usingShield = shielding;
    }
    public void UseShield() {
        if (!canUse) return;
        OnShieldUse.Invoke(currentCharge);
        if (shieldCost > currentCharge) {
            Debug.Log("Not enough charge");
            timeWithoutUse = durationTillRegen-1; // to stop usage when at 0 charge
            return;
        }
        hp.SetInvincibility(true);
        RemoveCharge(shieldCost);
    }
    public void AddCharge(float amt) {
        currentCharge += amt;
        if (currentCharge >= maxCharge) {
            currentCharge = maxCharge;
            canUse = true;
        }
    }
    public void RemoveCharge(float amt) {
        currentCharge -= amt;
        if (currentCharge < 0) {
            currentCharge = 0;
        }
    }
    public float GetMaxShieldCharge() {
        return maxCharge;
    }
    public bool GetUsingShield() {
        return usingShield;
    }
    public bool GetCanUseShield() {
        return canUse;
    }
}
