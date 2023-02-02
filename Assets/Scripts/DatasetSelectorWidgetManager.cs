using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DatasetSelectorWidgetManager : MonoBehaviour
{
    public RequisitionManager requisitionManager;
    public TMP_InputField iFDataset;
    public TMP_Dropdown dpdDataset;
    public TMP_Text txtConteudoDataset;

    private bool _searchEnabled = false;

    public void switchVisibility()
    {
        _searchEnabled = !_searchEnabled;

        iFDataset.gameObject.SetActive(_searchEnabled);
        dpdDataset.gameObject.SetActive(!_searchEnabled);
    }

    public void GetDatasetBySearchButton()
    {
        string nomeDataset = iFDataset.text;

        if (nomeDataset.Equals("")) return;

        Debug.Log("Searching by... " + nomeDataset);
        requisitionManager.GetDatasetPorNome(nomeDataset, true);

    }

    public void GetDatasetByDropodownOptions()
    {
        int dropdownIndex = dpdDataset.value - 1;
        string nomeDataset = dpdDataset.options[dpdDataset.value].text;

        if (dropdownIndex < 0) return;
        Debug.Log("Select the " + nomeDataset + " dataset");

        requisitionManager.GetDatasetPorNome(nomeDataset, true);
    }

    public void AtualizaTextoCanvas(string text)
    {
        txtConteudoDataset.text = text;
    }

}
