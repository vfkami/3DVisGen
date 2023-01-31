using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DatasetSelectorWidgetManager : MonoBehaviour
{
    private RequisitionManager rq;



    public TMP_InputField IfDataset;
    public TMP_Dropdown DpdDataset;

    public Button SwitchButton;
    public Button SearchButton;

    public TMP_Text TxtDatasetContent;

    bool searchEnabled = false;

    private void Start()
    {
        rq = GameObject.Find("SceneManager").GetComponent<RequisitionManager>();
    }

    public void switchVisibility()
    {
        searchEnabled = !searchEnabled;

        IfDataset.gameObject.SetActive(searchEnabled);
        DpdDataset.gameObject.SetActive(!searchEnabled);
    }

    public void GetDatasetBySearchButton()
    {
        string datasetName = IfDataset.text;

        if (datasetName.Equals("")) return;

        Debug.Log("Searching by... " + datasetName);
    }

    public void GetDatasetByDropodownOptions()
    {
        int dropdownIndex = DpdDataset.value - 1;
        string datasetName = DpdDataset.options[DpdDataset.value].text;

        if (dropdownIndex < 0) return;
        Debug.Log("Select the " + datasetName + " dataset");

    }

    public void GetDatasetContent()
    {
        
    }

}
