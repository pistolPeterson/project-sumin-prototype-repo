using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLineAttackPattern : AttackPattern
{
    private int currPos;

    protected override void Start() {
        base.Start();
        currPos = (int)playFieldPosConstraint;
    }
    public override void Attack() {
        SpawnInLineProjectile();
    }
    public void SpawnInLineProjectile() {
        InLinePatternAttack();
        Instantiate(projectilePrefab, projectileSpawnLoc, Quaternion.identity);
    }

    public void InLinePatternAttack() {
        SetSpawnLoc(currPos);
        currPos--;
        if (currPos < -playFieldPosConstraint) {
            currPos = (int)playFieldPosConstraint;
        }
    }
}
