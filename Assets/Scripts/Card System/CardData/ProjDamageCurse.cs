
using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/ProjDamageCurse")]
public class ProjDamageCurse : CurseCardBase
{
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.BossAttackHandler.ProjectileDamageBuffer = 1;
    }
}
