using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseVisual : MonoBehaviour
{
  [SerializeField] private GameObject pauseGameObj;


  public void Show()
  {
    pauseGameObj.SetActive(true);
  }

  public void Hide()
  {
    pauseGameObj.SetActive(false);

  }
}
