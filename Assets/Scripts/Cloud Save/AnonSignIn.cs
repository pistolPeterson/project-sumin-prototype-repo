using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using UnityEngine;

public class AnonSignIn : MonoBehaviour
{
    private async void InitializeUnityServicesAsync()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized!");
        }
        catch (Exception e)
        {
            Debug.LogError("Error initializing Unity Services: " + e.Message);
        }
    }
    
    void Start()
    {
       // StartCoroutine(InitializeUnityServicesAsync());

        // You can start other non-Unity Services related initialization tasks here
    }
}
