using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{


    private void Start()
    {
        FastGameMultiplayer.Instance.OnTryingToJoinGame += FastGameMultiplayer_OnTryingToJoinGame;
        FastGameMultiplayer.Instance.OnFailedToJoinGame += FastGameMultiplayer_OnFailedToJoinGame;
        Hide();
    }

    private void FastGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void FastGameMultiplayer_OnTryingToJoinGame(object sender, System.EventArgs e)
    {
        Show();
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
        FastGameMultiplayer.Instance.OnTryingToJoinGame -= FastGameMultiplayer_OnTryingToJoinGame;
        FastGameMultiplayer.Instance.OnFailedToJoinGame -= FastGameMultiplayer_OnFailedToJoinGame;
    }
}
