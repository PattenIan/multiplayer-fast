using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NetworkManagerUI : MonoBehaviour
{
    public static NetworkManagerUI network;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private static String activeJoinCode;

    private void Awake()
    {
        hostBtn.onClick.AddListener(async() =>
        {
            SceneManager.LoadScene("Lobby");
            var relayCreated = new TaskCompletionSource<bool>();
            TestRelay.CreateRelay(() => relayCreated.SetResult(true));

            await relayCreated.Task; // wait for relay creation to complete

            Debug.Log(TestRelay.GetJoinCode());
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
