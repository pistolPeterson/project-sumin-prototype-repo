
using UnityEngine;
[CreateAssetMenu(menuName = "Create Card Instance/EncounterDistBless")]
public class EncounterDistanceBless : BlessCardBase
{
    public override CardMappingEnum GetCardMappingEnum => CardMappingEnum.EncounterDistanceBless;

    public override void CardEffect(GameManager gameManager)
    {
        EncounterDistanceDataHandler encounterDistData = FindObjectOfType<EncounterDistanceDataHandler>();
        encounterDistData.EncounterDuration -= 5f;
    }
}
