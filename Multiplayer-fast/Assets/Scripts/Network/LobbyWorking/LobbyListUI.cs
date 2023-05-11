using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputLobbyCode;
    [SerializeField] private Button joinBtn;
    [SerializeField] private Button closeBtn;

    private void Awake()
    {
        joinBtn.onClick.AddListener(() =>
        {
            GameLobby.Instance.JoinWithCode(inputLobbyCode.text);
        });
        closeBtn.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
