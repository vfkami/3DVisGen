using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequisitionManager : MonoBehaviour
{
    public DatasetSelectorWidgetManager datasetWidget;
    public VisualizationRenderer visualization;

    public string uri;
    public static string enderecoServidor = "http://localhost";
    public static string porta = "3000";

    string respostaJson;
        
    // TODO: Adiciona validação de conexão com servidor
    void Start()
    {
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
        string request = $"chartgen.html?";
        string x = $"x={nomeEixoX}";
        string y = $"&y={nomeEixoY}";
        string chartType = $"&chart=barchartvertical";
        string title = $"&title={nomeEixoX} X {nomeEixoY}";
        string xLabel = $"&xlabel={nomeEixoX}";
        string yLabel = $"&xlabel={nomeEixoY}";
        string filterUri = $"&filter={filter}";

        string uri = $"{enderecoServidor}:{porta}/generate/{nomeDataset}/{request}{x}{y}{chartType}{title}{xLabel}{yLabel}{filterUri}";
        uri = uri.Replace(" ", "");
        Debug.Log(uri);
        StartCoroutine(GetRequest(uri, 5));

    }

    IEnumerator GetRequest(string uri, int operacao)
    {
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
                    ResponseCallback(webRequest.downloadHandler.text, operacao);
                    break;
            }
        }
    }

    private void ResponseCallback(string data, int operacao)
    {
        switch (operacao)
        {
            case 1: // GET Lista de Datasets
                string[] splitedData = data.Split(',');
                datasetWidget.AtualizaOpcoesDropdownDataset(splitedData);
                break;

            case 2: // GET Metadados Dataset Selecionado 
                datasetWidget.AtualizaTextoCanvas(data);
                DatasetManager.SetDataset(data);
                break;
            case 5: // GET Visualizacao BarChart
                visualization.RenderOfBase64(data);
                break;
            default:
                respostaJson = data;
                break;
        }

    }
}
