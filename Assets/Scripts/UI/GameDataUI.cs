
using TMPro;
using UnityEngine;

public class GameDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText; 
    [SerializeField] private TextMeshProUGUI currentCardsDisplayText;

    [SerializeField] private NodeMap nodeMap;

    private void Awake()
    {
        nodeMap.OnProgressUpdated.AddListener(UpdateProgressText);
        progressText.text = "---";
    }

    private void UpdateProgressText(int currentProgress, int maxProgress)
    {
        progressText.text = $"Night {currentProgress} of {maxProgress} ";
    }
}
