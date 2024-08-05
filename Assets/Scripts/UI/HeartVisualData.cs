using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartVisualData : MonoBehaviour
{
   private Image image;
   [field: SerializeField] public Sprite fullHeart { get; private set; }
   [field: SerializeField] public Sprite halfHeart { get; private set; }
   [field: SerializeField] public Sprite emptyHeart { get; private set; }
   
   private void Awake()
   {
      image = GetComponent<Image>();
   }

   public void SetImage(Sprite newSprite)
   {
      image.sprite = newSprite;
   }
}
