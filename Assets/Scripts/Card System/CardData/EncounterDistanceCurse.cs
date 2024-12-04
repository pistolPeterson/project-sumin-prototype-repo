
using UnityEngine;
[CreateAssetMenu(menuName = "Create Card Instance/EncounterDistanceCurse")]
public class EncounterDistanceCurse : CurseCardBase
{
    
    public override void CardEffect(GameManager gameManager)
    {
        EncounterDistanceDataHandler encounterDistData = FindObjectOfType<EncounterDistanceDataHandler>();
        encounterDistData.EncounterDuration += 5f;
    }
}
