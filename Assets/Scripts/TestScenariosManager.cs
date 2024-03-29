using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenariosManager : MonoBehaviour
{
    string _serverIp = "";
    string _dataset = "";
    string _eixoX = "";
    string _eixoY = "";
    string _cor = "";
    string _subVis= "";

    public RequisitionManager _requisitionManager;
    public SettingsManager _settingsManager;
    public DatasetManager _datasetManager;

    public void ConfiguraCenario01()
    {
        _serverIp = "127.0.0.1";
        _dataset = "base_regioes_brasil";
        _eixoX = "REGIAO";
        _eixoY = "NUM_MUNICIPIOS";
        _cor = "TAXA_ALFABETI_CAT";
        _subVis = "ESTADO";

        RealizaRequisicao();
    }

    public void ConfiguraCenario02()
    {
        _serverIp = "127.0.0.1";
        _dataset = "base_regioes_brasil";
        _eixoX = "REGIAO";
        _eixoY = "RENDA_PER_CAPITA";
        _cor = "DENS_DEMO_CAT";
        _subVis = "ESTADO";

        RealizaRequisicao();
    }

    public void RealizaRequisicao()
    {
        _requisitionManager.SetEnderecoServidor(_serverIp);
        _requisitionManager.GetDatasetPorNomeAtributosPreenchidos(_dataset);
        _settingsManager.SetQtdBarras(6);
    }

    public void DefineAtributosSelecionados()
    {
        DatasetManager.SetNomeDataset(_dataset);
        DatasetManager.SetNomeEixoX(_eixoX);
        DatasetManager.SetNomeEixoY(_eixoY);
        DatasetManager.SetNomeCor(_cor);
        DatasetManager.SetNomeAtributoSubVisualizacao(_subVis);

        _datasetManager.RequestVisualization();
        _datasetManager.RequestSubVisualization();
        _datasetManager.SendToArduino();

    }
}
