using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequisitionManager : MonoBehaviour
{
    public static string enderecoServidor = "http://localhost";
    public static string porta = "3000";

    public string uri;

    string respostaJson = "";
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Application Start...");

        getDatasetPorNome("automobile");
    }

    private void Update()
    {
        Debug.Log(respostaJson);

    }

    public IEnumerator getDatasetPorNome(string datasetName)
    {
        Debug.Log("start coroutine");
        string uri = enderecoServidor + ":" + porta + "/metadata/" + datasetName;
        yield return StartCoroutine(getRequest(uri));

        Debug.Log("coroutine finish!");


    }

     

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.LogError("Error while sending request for " + uri);
        else
            ResponseCallback(request.downloadHandler.text);
    }

    private void ResponseCallback(string data)
    {
        respostaJson = data;
    }
}
