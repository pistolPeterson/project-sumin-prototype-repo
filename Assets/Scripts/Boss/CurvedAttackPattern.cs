using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedAttackPattern : AttackPattern {

    [Header("Curved Attack")]
    [SerializeField] private float projectileCount = 5;
    [SerializeField] private float delayBetweenRandomStat = 1f;

    [Header("Stats")]
    [SerializeField] private float archHeight = 2f;
    [SerializeField] private float horizontalSpacing = 2f;
    [SerializeField] private float verticalSpacing = 1f;
    protected override void Attack() {
        attackComplete = false;
        SpawnProjectiles();
    }
    private void SpawnProjectiles() {
        //float xOrigin = transform.position.x + horizontalSpacing;
        //Instantiate(projectilePrefab, new Vector3(xOrigin, transform.position.y, 0), Quaternion.identity);
        float xOffset = horizontalSpacing * (projectileCount - 1) / 2;

        //StartCoroutine(ChooseRandomStats());

        /*for (int i = 0; i < projectileCount/2; i++) {
            float x = transform.position.x + xOffset + i * horizontalSpacing;
            float yOffset = verticalSpacing * ((i + 1) / 2);
            float y = transform.position.y + yOffset + archHeight * (1 - Mathf.Pow(i / (projectileCount - 1), 2));

            Instantiate(projectilePrefab, new Vector3(x, y, 0), Quaternion.identity);
            Instantiate(projectilePrefab, new Vector3(x, -y, 0), Quaternion.identity);
        }*/
        for (int i = 0; i < projectileCount; i++) {
            float x = transform.position.x + xOffset + i * horizontalSpacing; // Flipped x-position
            float y = transform.position.y - archHeight * (1 - Mathf.Pow((i - (projectileCount - 1) / 2) / ((projectileCount - 1) / 2), 2));
            Vector3 spawnPosition = new Vector3(x, y, 0);
            Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
            spawnPosition.y = -spawnPosition.y;
            Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        }
        attackComplete = true;
    }
    private IEnumerator ChooseRandomStats() {
        while (!attackComplete) {
            
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
    private float Clamp(float value) {
        return Mathf.Clamp(value, playFieldPosConstraint, -playFieldPosConstraint);
    }
    

}
