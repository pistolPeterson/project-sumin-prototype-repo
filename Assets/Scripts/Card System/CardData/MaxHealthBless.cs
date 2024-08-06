//Example health bless/curse template -> blessing and curse can be in the same script/file 

using UnityEngine;

[CreateAssetMenu(menuName = "Create Card Instance/MaxHealthBless")]
public class MaxHealthBless : BlessCardBase
{
    public override void CardEffect(GameManager gameManager)
    {
        gameManager.playerObject.GetComponent<PlayerHealth>().AddMaxHealth(2); //this would need to be called before its updated by UI
    }
}