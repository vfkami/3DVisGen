using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequisitionManager : MonoBehaviour
{
    public DatasetSelectorWidgetManager datasetWidget;

    public static string enderecoServidor = "http://localhost";
    public static string porta = "3000";

    public string uri;

    string respostaJson = "";
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Application Start...");

        getDatasetPorNome("automobile", true);
    }

    public void getDatasetPorNome(string datasetName, bool updateCanvas)
    {
        Debug.Log("start coroutine");
        string uri = enderecoServidor + ":" + porta + "/metadata/" + datasetName;
        StartCoroutine(GetRequest(uri, true));
    }


    IEnumerator GetRequest(string uri, bool updateText = false)
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
                    ResponseCallback(webRequest.downloadHandler.text);
                    if (updateText)
                        datasetWidget.AtualizaTextoCanvas(respostaJson);
                    break;
            }
        }
    }

    private void ResponseCallback(string data)
    {
        respostaJson = data;

        DatasetManager.SetDataset(data);
        Debug.Log(data);
    }
}
