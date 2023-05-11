using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterColorSingleUI : MonoBehaviour
{
    [SerializeField] private int colorId;
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedGameobject;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            FastGameMultiplayer.Instance.ChangeColorPlayer(colorId);
        });
    }

    private void Start()
    {
        FastGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += FastGameMultiplayer_OnPlayerDataNetworkListChanged;
        image.color = FastGameMultiplayer.Instance.GetPlayerColor(colorId);
        UpdateIsSelected();
    }

    private void FastGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e)
    {
        UpdateIsSelected();
    }

    private void UpdateIsSelected()
    {
        if(FastGameMultiplayer.Instance.GetPlayerData().colorId == colorId)
        {
            selectedGameobject.SetActive(true);
        }
        else
        {
            selectedGameobject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        FastGameMultiplayer.Instance.OnPlayerDataNetworkListChanged -= FastGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}
