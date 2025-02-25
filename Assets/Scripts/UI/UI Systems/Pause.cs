using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private PauseVisual pauseVisual;
    private void Awake()
    {
        pauseVisual = GetComponent<PauseVisual>();
    }
    
    [ProButton]
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseVisual.Show();

    }
    [ProButton]
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseVisual.Hide();
    }
}
