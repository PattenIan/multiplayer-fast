using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer BodyMeshRenderer;

    private Material material;

    private void Awake()
    {
        material = new Material(BodyMeshRenderer.material);
        BodyMeshRenderer.material = material;
    }
    public void SetPlayerColor(Color color)
    {
        material.color = color;
    }
}
