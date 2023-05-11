using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class TestRelay : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI joinTextField;
    [SerializeField] private TextMeshProUGUI joinCodeInput;

    static string joinCode;


    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public static async void CreateRelay(Action onCreated = null)
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            SetJoinCode(await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId));

            RelayServerData relayServarData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServarData);
            onCreated?.Invoke();
        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    private static void SetJoinCode(string s)
    {
        joinCode = s;
    }
    public static string GetJoinCode()
    {
        return joinCode;
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            
            RelayServerData relayServarData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServarData);
        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
