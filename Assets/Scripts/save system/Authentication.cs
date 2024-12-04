using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class Authentication : MonoBehaviour
{

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Authentication State: " + UnityServices.State);
        SetupAuthenticationEvents();
        await SignInAnon();
    }

    private void SetupAuthenticationEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
                
            //get player id
            Debug.Log($"Signed in! Player ID: {AuthenticationService.Instance.PlayerId} ");
            
            //get player access token
            Debug.Log($"Generating Access Token: {AuthenticationService.Instance.AccessToken} ");
        };

        AuthenticationService.Instance.SignInFailed += (err) =>
        {

            Debug.LogError($"Error Signing in, Raeus broke it: " + err);
        };
        
        
        AuthenticationService.Instance.SignedOut += () =>
        {

            Debug.Log($"Player has signed out");
        };
        
        AuthenticationService.Instance.Expired += () =>
        {

            Debug.Log($"Player session could not be refreshed and expired");
        };
    }

    private async Task SignInAnon()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        }
        catch (AuthenticationException ex)
        {
            Debug.LogError("auth err: " + ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError("request failed: " + ex);
        }
    }
  
    
}
