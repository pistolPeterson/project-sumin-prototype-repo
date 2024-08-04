using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHeatlh hp;
    [SerializeField] private GameObject heartsParentGroup;
    [SerializeField] private GameObject heartTemplate;
    public List<Image> hearts;
    [SerializeField] private int hpPerHeart = 2; // Must be divisible by 2
    private int lastHeartIndex = 0;

    [Header("Visual")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    private void Start() {
        if (hpPerHeart % 2 == 0)
            DisplayHealth();
        else Debug.LogWarning("HP per heart must be divisible by 2");
    }
    public void DisplayHealth() {
        int initialHP = hp.GetInitialHealth();
        int fullHearts = initialHP / hpPerHeart; // amount of full hearts
        int halfHearts = initialHP % hpPerHeart;
        halfHearts /= (hpPerHeart / 2); // amount of half hearts
        SpawnHearts(fullHearts, fullHeartSprite);
        if (halfHearts != 0) {
            SpawnHearts(halfHearts, halfHeartSprite);
        }
        lastHeartIndex = hearts.Count-1;
        lastHeartIndex = ClampLastHeartIndex(lastHeartIndex);
    }
    private void SpawnHearts(int amountHearts, Sprite sprite) {
        for (int i = 0; i < amountHearts; i++) {
            GameObject heart = Instantiate(heartTemplate, heartsParentGroup.transform);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = sprite;
            hearts.Add(heartImage);
        }
    }
    [ProButton]
    public void TakeDamage(int damageTaken) {
        int halfHearts = damageTaken / (hpPerHeart / 2);
        RemoveHearts(halfHearts);
    }
    private void RemoveHearts(int amountHearts) {
        for (int i = 0; i < amountHearts; i++) {            
            if (hearts[lastHeartIndex].sprite == halfHeartSprite) {
                hearts[lastHeartIndex].sprite = emptyHeartSprite;
                lastHeartIndex--;
                lastHeartIndex = ClampLastHeartIndex(lastHeartIndex);
            }
            else if (hearts[lastHeartIndex].sprite == fullHeartSprite) {
                hearts[lastHeartIndex].sprite = halfHeartSprite;
            }
        }
    }
    [ProButton]
    public void Heal(int healAmount) {
        int halfHearts = healAmount / (hpPerHeart / 2);
        AddHearts(halfHearts);
    }
    private void AddHearts(int amountHearts) {
        for (int i = 0; i < amountHearts; i++) {
            if (hearts[lastHeartIndex].sprite == halfHeartSprite) {
                hearts[lastHeartIndex].sprite = fullHeartSprite;
                lastHeartIndex++;
                lastHeartIndex = ClampLastHeartIndex(lastHeartIndex);
            } else if (hearts[lastHeartIndex].sprite == emptyHeartSprite) { 
                hearts[lastHeartIndex].sprite = halfHeartSprite;
            }
        }
    }
    private int ClampLastHeartIndex(int value) {
        return Mathf.Clamp(value, 0, hearts.Count-1);
    }
}
