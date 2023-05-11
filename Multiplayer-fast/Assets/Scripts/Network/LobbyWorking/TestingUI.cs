using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingUI : MonoBehaviour
{
    private void Start()
    {
        FastGameManager.Instance.OnStateChanged += FastGameManager_OnStateChanged;
    }

    private void FastGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (FastGameManager.Instance.IsLocalPlayerReady())
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
