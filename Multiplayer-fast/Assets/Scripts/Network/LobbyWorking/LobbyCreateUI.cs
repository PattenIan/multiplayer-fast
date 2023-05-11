using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button createBtn;
    [SerializeField] private Toggle accessToggle;
    [SerializeField] private TMP_InputField inputLobbyName;
    [SerializeField] private TMP_InputField inputMaxPlayers;

    private void Awake()
    {
        createBtn.onClick.AddListener(() =>
        {
            GameLobby.Instance.CreateLobby(inputLobbyName.text, false);
        });
        closeBtn.onClick.AddListener(() =>
        {
            Hide();
        });

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
