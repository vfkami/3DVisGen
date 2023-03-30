using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class FiducialMarkerController : MonoBehaviour
{
    public TextMeshProUGUI markerText;
    public Image image;
    public Canvas canvas;

    //by default, canvas start the scene disabled;
    private void Start()
    {
        canvas.enabled = false;
        //SetModoImagem();
    }
    public void SetModoTexto()
    {
        markerText.gameObject.SetActive(true);
        image.gameObject.SetActive(false);
    }
    public void SetModoImagem()
    {
        markerText.gameObject.SetActive(false);
        image.gameObject.SetActive(true);   
    }

    public void SetTexto(string text)
    {
        markerText.text = text;
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    void OnMouseDown()
    {
        canvas.enabled = !canvas.enabled;
    }

}
