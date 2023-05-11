using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    private void Start()
    {
        FastGameManager.Instance.OnLocalPlayerReadyChanged += FastGameManager_OnLocalPlayerReadyChanged;
        FastGameManager.Instance.OnStateChanged += FastGameManager_OnStateChanged;

        Hide();
    }

    private void FastGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (FastGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void FastGameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
    {
        if (FastGameManager.Instance.IsLocalPlayerReady())
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
