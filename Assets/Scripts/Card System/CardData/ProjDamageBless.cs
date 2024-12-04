
using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/ProjDamageBless")]
public class ProjDamageBless : BlessCardBase
{
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.BossAttackHandler.ProjectileDamageBuffer = -1;
    }
}
