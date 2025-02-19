using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : PersistentSingleton<Authentication>
{
    private bool eventsInitiliazed = false;
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Starting Client Service");
        StartClientService();
    }

    public async void StartClientService()
    {

        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                var options = new InitializationOptions();
                options.SetProfile("default_profile");
                await UnityServices.InitializeAsync();
            }

            if (!eventsInitiliazed)
            {
                SetupEvents();
            }
            
            if (AuthenticationService.Instance.SessionTokenExists)
            {

                SignInAnonymouslyAsync();
            }
            else
            {
                
            }
            
        }
        catch (Exception e)
        {
           
           Debug.LogError("Error with Starting CLient Service " + e);
        }
    }


    public async void SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException exception)
        {
            Debug.LogError("Failed to Sign in " + exception);
        }
        catch (RequestFailedException exception)
        {
            Debug.LogError("Failed to connect to network " + exception);
        }
    }

    private async void SignInConfirmAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(AuthenticationService.Instance.PlayerName))
            {
                await AuthenticationService.Instance.UpdatePlayerNameAsync("Pete-Player");
                Debug.Log("Signed in to UNITY SDK");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    private void SetupEvents()
    {
       
        eventsInitiliazed = true;
        AuthenticationService.Instance.SignedIn += () =>
        {
            SignInConfirmAsync();
        };
        AuthenticationService.Instance.SignedOut += () => { };
        AuthenticationService.Instance.Expired += () =>
        {
            SignInAnonymouslyAsync();
        };
    }
}
