using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/MaxHealthCurse")]
public class MaxHealthCurse : CurseCardBase
{
    
    public override CardMappingEnum GetCardMappingEnum => CardMappingEnum.MaxHealthCurse;
    //TODO: add a check for leaving a min of 2 hearts
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.playerObject.GetComponent<PlayerHealth>().SubtractMaxHealth(2); //this would need to be called before its updated by UI
    }
}