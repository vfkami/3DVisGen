using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FiducialMarkerController : MonoBehaviour
{
    public TextMeshProUGUI markerText;
    public Canvas canvas;

    //by default, canvas start the scene disabled;
    private void Start()
    {
        canvas.enabled = false;
    }

    public void SetTexto(string text)
    {
        markerText.text = text;
    }

    void OnMouseDown()
    {
        canvas.enabled = !canvas.enabled;
    }

}
