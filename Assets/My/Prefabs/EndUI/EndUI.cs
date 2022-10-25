using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public void DrawResult(bool isWin)
    {
        textMesh.text = isWin ? "Win" : "Loss";
    }
}
