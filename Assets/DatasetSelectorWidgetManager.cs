using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DatasetSelectorWidgetManager : MonoBehaviour
{
    public RequisitionManager rq;

    public TMP_InputField IfDataset;
    public TMP_Dropdown DpdDataset;

    public TMP_Text TxtDatasetContent;

    bool searchEnabled = false;

    public void switchVisibility()
    {
        searchEnabled = !searchEnabled;

        IfDataset.gameObject.SetActive(searchEnabled);
        DpdDataset.gameObject.SetActive(!searchEnabled);
    }

    public void GetDatasetBySearchButton()
    {
        string nomeDataset = IfDataset.text;

        if (nomeDataset.Equals("")) return;

        Debug.Log("Searching by... " + nomeDataset);
        rq.getDatasetPorNome(nomeDataset, true);

    }

    public void GetDatasetByDropodownOptions()
    {
        int dropdownIndex = DpdDataset.value - 1;
        string nomeDataset = DpdDataset.options[DpdDataset.value].text;

        if (dropdownIndex < 0) return;
        Debug.Log("Select the " + nomeDataset + " dataset");

        rq.getDatasetPorNome(nomeDataset, true);

    }

    public void AtualizaTextoCanvas(string text)
    {
        TxtDatasetContent.text = text;
    }

}
