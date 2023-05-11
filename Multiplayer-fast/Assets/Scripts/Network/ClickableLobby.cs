using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class ClickableLobby : MonoBehaviour
{
    Lobby lobby;
    LobbyGame lg;
    TMP_Text[] tt;
    GameObject inputfield;


        public Lobby Lobby { get; private set; }

        public void SetLobby(Lobby lobby)
        {
            Lobby = lobby;
        }
    

    private void Awake()
    {
        inputfield = GameObject.Find("LobbyCode Input");
        lobby = Lobby;
        Debug.Log(lobby + " This is goood lobby");
        lg = FindObjectOfType<LobbyGame>();
    }

    public void ClickedOnLobby()
    {
        tt = inputfield.GetComponentsInChildren<TMP_Text>();
        string lobbyCode = tt[1].text;
        if (lobbyCode.Contains("\u200b"))
        {
            lobbyCode = lobbyCode.Replace("\u200b".ToString(), "");
        }
        Debug.Log(lobbyCode.Trim() + " This is the code!!!!");
        //lg.JoinLobbyByCode(lobbyCode.Trim());
    }
}
