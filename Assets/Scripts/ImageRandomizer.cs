using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ImageRandomizer : MonoBehaviour
{
    public List<Sprite> sprites;


    private void Start()
    {
        GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
