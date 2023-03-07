using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{
    static private Dataset _dataset;

    static private string _nomeDataset;
    static private string _nomeEixoX;
    static private string _nomeEixoY;

    static private string[] _eixoX;
    static private string[] _eixoY;

    static private string _filtrosUri;

    private static AxisConfigurationWidgetManager _axisWidget;
    private static FilterConfigurationWidgetManager _filterWidget;
    private static RequisitionManager _requisitionManager;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");

        _axisWidget = canvas.GetComponentInChildren<AxisConfigurationWidgetManager>();
        _filterWidget = canvas.GetComponentInChildren<FilterConfigurationWidgetManager>();
        _requisitionManager = GameObject.Find("SceneManager").GetComponent<RequisitionManager>();

    }
    public static void SetDataset(string json)
    {
        try
        {
            _dataset = Utils.CriaDoJSON(json);
        }
        catch (ArgumentException ex)
        {
            _dataset = new Dataset()
            {
                columns = new string[0],
                rows = 0,
                meta = new Metadata[0]
            };

            Debug.LogError(ex.Message);
        }

        AtualizaElementosCanvas();
    }

    public static void SetNomeDataset(string nome)
    {
        _nomeDataset = nome;
    }

    public static void SetNomeEixoX(string eixo)
    {
        _nomeEixoX = eixo;
    }
    
    public static void SetNomeEixoY(string eixo)
    {
        _nomeEixoY = eixo;
    }

    public static void AtualizaElementosCanvas()
    {
        _filterWidget.SetLabelsFiltro(_dataset.meta
            .Select(i => i.name.ToString()).ToArray());

        _filterWidget.SetTipoFiltros(_dataset.meta
            .Select(i => i.type.ToString()).ToArray());

        _filterWidget.SetInfoFiltros(_dataset.meta
            .Select(i => i.extent.ToArray<string>()).ToArray());

        List<string> categoricLabels = new List<string>();
        List<string> numericLabels = new List<string>();


        for (int i = 0; i < _dataset.meta.Length; i++)
        {
            var column = _dataset.meta[i];

            if (column.type.Equals("categorical"))
            {
                categoricLabels.Add(column.name);
            }
            if (column.type.Equals("numeric"))
            {
                numericLabels.Add(column.name);
            }
        }

        _axisWidget.SetLabelsAtributoCor(
            _dataset.meta.Select(i => i.name.ToString()).ToArray());

        _axisWidget.SetLabelsAtributoEixoX(categoricLabels.ToArray()); 
        _axisWidget.SetLabelsSubVisualization(categoricLabels.ToArray());

        _axisWidget.SetLabelsAtributoEixoY(numericLabels.ToArray());
    }

    public static void RequestVisualization()
    {
        //Passo 1: Reunir dados da base
        if (string.IsNullOrEmpty(_nomeDataset))
        {
            Debug.LogError("Nenhum dataset selecionado. Escolha um e tente novamente!");
            return;
        }

        if (_dataset == null) 
        {
            Debug.LogError("O dataset retornou nulo. Selecione outro dataset ou tente novamente!");
            return;
        }

        // Passo 2: Reunir dados do eixo x e y
        if (string.IsNullOrEmpty(_nomeEixoX) || string.IsNullOrEmpty(_nomeEixoX))
        {
            Debug.LogError("Um dos eixos não foi definido. Use o menu e selecione um dos atributos disponíveis!");
            return;
        }

        _filtrosUri = _filterWidget.GetFiltrosConfigurados();

        _requisitionManager.RequestVisualization(
            nomeDataset: _nomeDataset,
            nomeEixoX: _nomeEixoX,
            nomeEixoY: _nomeEixoY,
            filter: _filtrosUri
            );
    }
}

[System.Serializable]
public class Dataset
{
    public string[] columns;
    public int rows;
    public Metadata[] meta;
}

[System.Serializable]
public class Metadata
{
    public string name;
    public string type;
    public string[] extent;
}
