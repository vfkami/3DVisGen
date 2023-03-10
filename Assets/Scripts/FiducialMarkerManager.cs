using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiducialMarkerManager : MonoBehaviour
{
    private const int NumeroMarcadores = 8;

    private Sprite[] sprites = new Sprite[NumeroMarcadores];
    private int _indexUltimoSpriteAdicionado;

    public GameObject modeloMarcador;
    public GameObject[] fiducialMarkers = new GameObject[NumeroMarcadores];

    private void Start()
    {
        _indexUltimoSpriteAdicionado = 0;
    }

    public void SetTextoMarcadorPorIndex(string text, int index)
    {
        fiducialMarkers[index].GetComponent<FiducialMarkerController>().SetTexto(text);
    }

    public void SetSpriteMarcadorPorIndex(Sprite sprite, int index)
    {
        try
        {
            fiducialMarkers[index].GetComponent<FiducialMarkerController>().SetImage(sprite);
            _indexUltimoSpriteAdicionado++;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void SetTextoTodosMarcadores(string[] texts)
    {
        int count = 0;

        if (texts.Length > NumeroMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado excede a quantidade de marcadores na cena. Os textos excedentes ser?o ignorados");
            
            count = NumeroMarcadores;
        }
        else if (texts.Length < NumeroMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado ? menor que a quantidade de marcadores na cena. Haver?o marcadores sem texto");
            
            count = texts.Length;
        }

        for(int i=0; i < count; i++)
            SetTextoMarcadorPorIndex(texts[i], i);

        return;        
    }

    public void SetTextoTodosMarcadores(Sprite[] sprites)
    {
        int count = 0;

        if (sprites.Length > NumeroMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado excede a quantidade de marcadores na cena. Os textos excedentes ser?o ignorados");

            count = NumeroMarcadores;
        }
        else if (sprites.Length < NumeroMarcadores)
        {
            Debug.LogWarning(
                $"A quantidade de texto passado ? menor que a quantidade de marcadores na cena. Haver?o marcadores sem texto");

            count = sprites.Length;
        }

        for (int i = 0; i < count; i++)
            SetSpriteMarcadorPorIndex(sprites[i], i);

        return;
    }

    public void ResetListaSubvisualizacao()
    {
        _indexUltimoSpriteAdicionado = 0;
        sprites = new Sprite[NumeroMarcadores];
    }

    public void AddNovaSubVisualizacao(Sprite sp)
    {
        if(_indexUltimoSpriteAdicionado >= NumeroMarcadores)
        {
            Debug.LogError("Lista de Sprites cheia. Os proximos n?o ser?o adicionados");
        }

        SetSpriteMarcadorPorIndex(sp, _indexUltimoSpriteAdicionado);
    }


}
