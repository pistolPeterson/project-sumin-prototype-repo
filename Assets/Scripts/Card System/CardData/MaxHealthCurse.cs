using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/MaxHealthCurse")]
public class MaxHealthCurse : CurseCardBase
{
    //TODO: add a check for leaving a min of 2 hearts
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.playerObject.GetComponent<PlayerHeatlh>().SubtractMaxHealth(2); //this would need to be called before its updated by UI
    }
}