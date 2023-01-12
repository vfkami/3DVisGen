using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiducialMarkerManager : MonoBehaviour
{
    public GameObject modeloMarcador;
    private int _qtdMarcadores;

    public GameObject[] fiducialMarkers = new GameObject[8];

    public void SetTextoMarcadorPorIndex(string text, int index)
    {
        fiducialMarkers[index].GetComponent<FiducialMarkerController>().SetText(text);
    }

    public void SetTextoTodosMarcadores(string[] texts)
    {
        int count = 0;

        if (texts.Length > _qtdMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado excede a quantidade de marcadores na cena. Os textos excedentes serão ignorados");
            count = _qtdMarcadores;
        }
        else if (texts.Length < _qtdMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado é menor que a quantidade de marcadores na cena. Haverão marcadores sem texto");
            count = texts.Length;
        }

        for(int i=0; i < count; i++)
            SetTextoMarcadorPorIndex(texts[i], i);

        return;        
    }



}
