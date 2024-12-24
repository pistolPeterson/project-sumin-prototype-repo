using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveByHover : MonoBehaviour
{
   private EventTrigger eventTrigger;
   private CameraController cameraController;

   private void Awake()
   {
      cameraController = FindObjectOfType<CameraController>();
   }


   public void OnStop()
   {
      cameraController.SetHorizontalInput(0);
   }
   public void OnGoRight()
   {
      cameraController.SetHorizontalInput(1);
   }
   
   public void OnGoLeft()
   {
      cameraController.SetHorizontalInput(-1);
   }
}
