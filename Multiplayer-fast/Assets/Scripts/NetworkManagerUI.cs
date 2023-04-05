using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private TextMeshProUGUI joinTextField;

    private void Awake()
    {
        hostBtn.onClick.AddListener(async() =>
        {

            var relayCreated = new TaskCompletionSource<bool>();
            TestRelay.CreateRelay(() => relayCreated.SetResult(true));

            await relayCreated.Task; // wait for relay creation to complete

            joinTextField.text = TestRelay.GetJoinCode();
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
