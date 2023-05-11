using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class SpawnLobbies : MonoBehaviour
{
    [SerializeField] GameObject activeLobby;
    TMP_Text[] labels;
    public Lobby lobby;

    public void SpawnLobby(string lobbyName, int maxplayers, int activePlayers, string lobbyCode, Lobby l)
    {
        GameObject newLobby = Instantiate(activeLobby, transform);
        labels = newLobby.GetComponentsInChildren<TMP_Text>();
        Debug.Log(labels.Length);
        labels[0].text = lobbyName;
        labels[1].text = activePlayers.ToString() + "/" + maxplayers.ToString();
        labels[2].text = lobbyCode;
        Debug.Log(labels[2].text);

        ClickableLobby clickableLobby = newLobby.GetComponent<ClickableLobby>();
        clickableLobby.SetLobby(l);

        Debug.Log(l.ToString() + " lobby name thing");
    }

    public void RefreshAllLobies()
    {
        int childs = transform.childCount;
        if(childs > 0)
        {
            for(int i = 0; i < childs; i++)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public Lobby GetLobby()
    {
        return lobby;
    }
}
