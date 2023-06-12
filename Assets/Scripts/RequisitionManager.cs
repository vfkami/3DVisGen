using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequisitionManager : MonoBehaviour
{
    public DatasetManager dataserManager;
    public DatasetSelectorWidgetManager datasetWidget;
    public VisualizationRenderer visualization;
    public FiducialMarkerManager fiducialMarkerManager;

    public string enderecoServidor = "localhost";
    public string porta = "3000";

    string respostaJson;
        
    // TODO: Adicionar validação de conexão com servidor
    void Start()
    {
        //GetDatasetsDisponiveis();
    }

    public void SetEnderecoServidor(string endereco)
    {
        enderecoServidor = "http://" + endereco;
        GetDatasetsDisponiveis();
    }

    public void GetDatasetsDisponiveis()
    {
        string uri = $"{enderecoServidor}:{porta}/info.html";
        StartCoroutine(GetRequest(uri, 1));
    }

    public void GetDatasetPorNome(string datasetName, bool updateCanvas)
    {
        string uri = $"{enderecoServidor}:{porta}/metadata/{datasetName}";
        StartCoroutine(GetRequest(uri, 2));
    }

    public void RequestVisualization(string nomeDataset, string nomeEixoX, string nomeEixoY, string filter)
    {
        string request = $"chartgen.png?";
        string x = $"x={nomeEixoX}";
        string y = $"&y={nomeEixoY}";
        string chartType = $"&chart=barchartvertical";
        string title = $"&title={nomeEixoX} X {nomeEixoY}";
        string xLabel = $"&xlabel={nomeEixoX}";
        string yLabel = $"&xlabel={nomeEixoY}";
        string filterUri = $"&filter={filter}";

        string uri = $"{enderecoServidor}:{porta}/generate/{nomeDataset}/{request}{x}{y}{chartType}{title}{xLabel}{yLabel}{filterUri}";
        uri = uri.Replace(" ", "");
        StartCoroutine(GetRequest(uri, 5));

    }

    public void RequestVisualization(string nomeDataset, string nomeEixoX, string nomeEixoY, string cor, string filter)
    {
        string request = $"chartgen.png?";
        string x = $"x={nomeEixoX}";
        string y = $"&y={nomeEixoY}";
        string chartType = $"&chart=barchartvertical";
        string title = $"&title={nomeEixoX} X {nomeEixoY}";
        string xLabel = $"&xlabel={nomeEixoX}";
        string yLabel = $"&xlabel={nomeEixoY}";
        string color = $"&color={cor}";
        string filterUri = $"&filter={filter}";

        string uri = $"{enderecoServidor}:{porta}/generate/{nomeDataset}/{request}{x}{y}{color}{chartType}{title}{xLabel}{yLabel}{filterUri}";
        uri = uri.Replace(" ", "");
        StartCoroutine(GetRequest(uri, 5));

    }

    // TODO: Adicionar gerenciador/renderizador de subvisualizacoes
    // TODO: Adicionar gerenciador dos botões virtuais - marcadores
    public void RequestSubVisualization(string nomeDataset, string nomeEixoX, string nomeEixoY, string filter)
    {
        string request = $"chartgen.png?";
        string x = $"x={nomeEixoX}";
        string y = $"&y={nomeEixoY}";
        string chartType = $"&chart=piechart";
        string title = $"&title={nomeEixoX} X {nomeEixoY}";
        string xLabel = $"&xlabel={nomeEixoX}";
        string yLabel = $"&xlabel={nomeEixoY}";
        string filterUri = $"&filter={filter}";

        string uri = $"{enderecoServidor}:{porta}/generate/{nomeDataset}/{request}{x}{y}{chartType}{title}{xLabel}{yLabel}{filterUri}";
        uri = uri.Replace(" ", "");
        Debug.Log(uri);

        StartCoroutine(GetRequest(uri, 6));

    }

    IEnumerator GetRequest(string uri, int operacao)
    {
        if (string.IsNullOrEmpty(enderecoServidor))
        { 
            Debug.LogError("endereço do servidor não definido");
            throw new System.Exception();
        }
        
        Debug.Log(uri);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("Error: " + webRequest.error);
                    datasetWidget.AtualizaTextoCanvas(webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    ResponseCallback(webRequest.downloadHandler, operacao);
                    break;
            }
        }
    }

    private void ResponseCallback(DownloadHandler data, int operacao)
    {
        switch (operacao)
        {
            case 1: // GET Lista de Datasets
                string[] splitedData = data.text.Split(',');
                datasetWidget.AtualizaOpcoesDropdownDataset(splitedData);
                break;

            case 2: // GET Metadados Dataset Selecionado 
                datasetWidget.AtualizaTextoCanvas(data.text);
                dataserManager.SetDataset(data.text);
                break;
            case 5: // GET Visualizacao BarChart
                byte[] response = data.data;
                visualization.RenderOfBytes(response);
                break;
            case 6:
                byte[] res = data.data;
                Sprite sprite = Utils.RenderOfBytes(res);
                fiducialMarkerManager.AddNovaSubVisualizacao(sprite);
                break;
            default:
                respostaJson = data.text;
                break;
        }

    }
}
