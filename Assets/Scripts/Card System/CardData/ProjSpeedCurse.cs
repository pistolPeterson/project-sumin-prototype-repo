using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/ProjSpeedCurse")]
public class ProjSpeedCurse : CurseCardBase
{
    
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.BossAttackHandler.ProjectileSpeedState = ProjectileSpeedUpgradeEnum.HIGH_SPEED;
    }
}