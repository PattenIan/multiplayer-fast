using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        FastGameMultiplayer.Instance.OnFailedToJoinGame += FastGameMultiplayer_OnFailedToJoinGame;
        GameLobby.Instance.OnCreatedLobbyStarted += GameLobby_OnCreatedLobbyStarted;
        GameLobby.Instance.OnCreatedLobbyFailed += GameLobby_OnCreatedLobbyFailed;
        GameLobby.Instance.OnJoinFailed += GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed += GameLobby_OnQuickJoinFailed;
        GameLobby.Instance.OnJoinStarted += GameLobby_OnJoinStarted;
        Show();
        Hide();
    }

    private void GameLobby_OnQuickJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Failed to join lobby!");
    }

    private void GameLobby_OnJoinStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Joining...");
    }

    private void GameLobby_OnJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("No active public servers...");
    }

    private void GameLobby_OnCreatedLobbyFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Failed to create a lobby!");
    }

    private void GameLobby_OnCreatedLobbyStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Creating Lobby...");
    }

    private void FastGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        if(NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Failed to connect");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
        messageText.text = NetworkManager.Singleton.DisconnectReason;

        if(messageText.text == "")
        {
            messageText.text = "Failed to connect";
        }
    }

    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        {
            FastGameMultiplayer.Instance.OnFailedToJoinGame -= FastGameMultiplayer_OnFailedToJoinGame;
        }
    }
}
