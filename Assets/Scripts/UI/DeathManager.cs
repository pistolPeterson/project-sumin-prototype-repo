using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHp;
    [SerializeField] private GameObject deathUI;

    private void Start() {
        HideDeathUI();
        playerHp = FindObjectOfType<PlayerHealth>();
        playerHp.OnDeath.AddListener(ShowDeathUI);
    }
    public void ShowDeathUI() {
        deathUI.SetActive(true);
    }
    public void HideDeathUI() {
        deathUI.SetActive(false);
    }
    public void DEBUG_Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
