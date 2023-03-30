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
    static private string _nomeAtributoSubVisualizacao;
    static private int _indexAtributoSubVisualizacao;

    static private string[] _eixoX;
    static private string[] _eixoY;

    static private string _filtrosUri;
    static private string[] _filtrosUriSubVisualizacao;

    public AxisConfigurationWidgetManager _axisWidget;
    public FilterConfigurationWidgetManager _filterWidget;
    public RequisitionManager _requisitionManager;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
    }
    public void SetDataset(string json)
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

    public static void SetNomeAtributoSubVisualizacao(int indexAtributo, string nomeAtributo)
    {
        _nomeAtributoSubVisualizacao = nomeAtributo;
        _indexAtributoSubVisualizacao = indexAtributo;
        //
    }

    public void AtualizaElementosCanvas()
    {
        _filterWidget.gameObject.SetActive(true);
        _axisWidget.gameObject.SetActive(true);

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

        _filterWidget.gameObject.SetActive(false);
        _axisWidget.gameObject.SetActive(false);
    }

    public void RequestVisualization()
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
            Debug.LogError("Um dos eixos n�o foi definido. Use o menu e selecione um dos atributos dispon�veis!");
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

    public void RequestSubVisualization()
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
            Debug.LogError("Um dos eixos n�o foi definido. Use o menu e selecione um dos atributos dispon�veis!");
            return;
        }


        _filtrosUriSubVisualizacao = _filterWidget
             .GetFiltrosConfiguradosParaSubvisualizacao(_nomeAtributoSubVisualizacao);

        if (_filtrosUriSubVisualizacao == null)
        {
            Debug.LogError("Houve algum erro ao gerar a string das subvisualiza��es. A requisi��o n�o ser� feita!");
            return;
        }

        foreach (var subvisualizacaouri in _filtrosUriSubVisualizacao)
        {
            _requisitionManager.RequestSubVisualization(
            nomeDataset: _nomeDataset,
            nomeEixoX: _nomeEixoX,
            nomeEixoY: _nomeEixoY,
            filter: subvisualizacaouri
            );
        }
        
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
