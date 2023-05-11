using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button createLobbyBtn;
    [SerializeField] private Button quickJoinBtn;
    [SerializeField] private Button joinByCodeBtn;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private LobbyListUI lobbyListUI;
    [SerializeField] private Transform lobbyListSingleUI;
    [SerializeField] private Transform lobbyListOfSingleUI;
    [SerializeField] private TMP_InputField inputPlayerName;

    private void Awake()
    {
        mainMenuBtn.onClick.AddListener(() => {
            GameLobby.Instance.LeaveLobby();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        createLobbyBtn.onClick.AddListener(() => {
            lobbyCreateUI.Show();
        });
        quickJoinBtn.onClick.AddListener(() => {
            GameLobby.Instance.QuickJoin();
        });
        joinByCodeBtn.onClick.AddListener(() => {
            lobbyListUI.Show();
        });

        lobbyListSingleUI.gameObject.SetActive(false);
    }

    private void Start()
    {
        inputPlayerName.text = FastGameMultiplayer.Instance.GetPlayerName();
        inputPlayerName.onValueChanged.AddListener((string newText) => {
            FastGameMultiplayer.Instance.SetPlayerName(newText);
        });

        GameLobby.Instance.OnLobbyListChanged += GameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void GameLobby_OnLobbyListChanged(object sender, GameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach (Transform child in lobbyListOfSingleUI)
        {
            if (child == lobbyListSingleUI) continue;
            Destroy(child.gameObject);
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyListSingleUI, lobbyListOfSingleUI);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }

    private void OnDestroy()
    {
        GameLobby.Instance.OnLobbyListChanged -= GameLobby_OnLobbyListChanged;
    }

}
