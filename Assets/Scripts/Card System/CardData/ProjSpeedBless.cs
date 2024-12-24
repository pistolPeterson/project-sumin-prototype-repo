using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/ProjSpeedBless")]
public class ProjSpeedBless : BlessCardBase
{
    public override CardMappingEnum GetCardMappingEnum => CardMappingEnum.ProjSpeedBless;
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.BossAttackHandler.ProjectileSpeedState = ProjectileSpeedUpgradeEnum.LOW_SPEED;
    }
}