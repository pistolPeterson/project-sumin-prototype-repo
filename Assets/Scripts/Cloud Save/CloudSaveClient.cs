//Using code from -> https://gist.github.com/Matthew-J-Spencer/336fcb9b3c06dc17fdd5834ca8251b35
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services;
using Unity.Services.CloudSave;
using UnityEngine;
using System;
using System.Collections;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class CloudSaveClient : MonoBehaviour //convert to singleton?
{


    //TODO: add a global stopper of doing the api calls
    //duplicated code 
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private async void Start()
    {
       await PeteCloudSave("peteKey", "rae is a bum");
       await LoadData("peteKey");
    }
    private async Task SaveData(Dictionary<string, object> newPlayerData)
    {
       
        await CloudSaveService.Instance.Data.Player.SaveAsync(newPlayerData);
        Debug.Log($"Saved data {string.Join(',', newPlayerData)}");
    }

    public async Task PeteCloudSave(string key, object value)
    {
        //force only simple types? 
        var playerData = new Dictionary<string, object>{
            {key, value}
        };
        await SaveData(playerData);
    }

    private async Task LoadData(string key)
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {
          key
        });

        if (playerData.TryGetValue(key, out var firstKey)) {
            Debug.Log($"firstKeyName value: {firstKey.Value.GetAs<string>()}");
        }

        
    }


}