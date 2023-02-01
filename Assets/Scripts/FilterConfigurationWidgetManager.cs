using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using System.Linq;

public class FilterConfigurationWidgetManager : MonoBehaviour
{
    private static string CATEGORIC = "categorical";
    private static string NUMERIC = "numeric";

    private GameObject[] filters;

    private string[] filterLabels;
    private string[] filterType;
    private string[][] filterInformation;
    
    public TMP_Dropdown filtroSelector;

    public GameObject ancoraFiltros;
    public GameObject CategoricTemplate;
    public GameObject NumericTemplate;

    private Filtro[] filtros;

    public void SetLabelsFiltro(string[] labels)
    {
        if (filters != null)
            filters.Where(go => go != null).ToList()
                .ForEach(go => Destroy(go));

        filterLabels = labels;
        filters = new GameObject[labels.Length];

        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        newOptions.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            newOptions.Add(option);
        }

        filtroSelector.ClearOptions();
        filtroSelector.AddOptions(newOptions);
    }

    // accept only: categoric, numeric
    public void SetTipoFiltros(string[] tipos)
    {
        filterType = tipos;
    }

    public void SetInfoFiltros(string[][] infos)
    {
        filterInformation = infos;
    }

    private string[] GetLabelAtributosCategoricos(int index)
    {
        if (!filterType[index].Equals(CATEGORIC))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return new string[0];

        }

        return filterInformation[index];
    }

    private Vector2 GetRangeNumerico(int index)
    {
        if (!filterType[index].Equals(NUMERIC))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return new Vector2(0, 0);
        }

        var extent = filterInformation[index];

        float min = 0;
        float max = 0;

        float.TryParse(extent[0], out min);
        float.TryParse(extent[1], out max);

        return new Vector2(min, max);
    }

    public void OnFiltroSelectorValueChanged()
    {
        filters.Where(go => go != null).ToList()
            .ForEach(go => go.SetActive(false));

        var index = filtroSelector.value - 1;
        if (index < 0) return;

        if (filters[index] == null)
        {        
            if (filterType[index].Contains("categoric"))
            {
                filters[index] = Instantiate(original: CategoricTemplate,
                parent: ancoraFiltros.transform,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
                );

                filters[index].name = filterLabels[index] + "_cat_" + index;
                filters[index].transform.localPosition = Vector3.zero;
                filters[index].GetComponent<CategoricFilterConfiguration>().
                    SetOptions(GetLabelAtributosCategoricos(index));
            }
            else
            {
                filters[index] = Instantiate(original: NumericTemplate,
                parent: ancoraFiltros.transform,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
                );

                filters[index].name = filterLabels[index] + "_num_" + index;
                filters[index].transform.localPosition = Vector3.zero;
                filters[index].GetComponent<NumericFilterConfiguration>().
                    SetOptions(GetRangeNumerico(index));
            }
        }

        filters[index].SetActive(true);
    }

    public string GetFiltrosConfigurados()
    {
        filtros = new Filtro[filterLabels.Length];


        for(int i = 0; i < filterLabels.Length; i++)
        {
            CategoricFilterConfiguration catConf;
            NumericFilterConfiguration numConf;

            if(filters[i] != null)
            {
                filters[i].TryGetComponent<CategoricFilterConfiguration>(out catConf);
                filters[i].TryGetComponent<NumericFilterConfiguration>(out numConf);

                if (catConf != null)
                {
                    filtros[i] = new Filtro(filterLabels[i], catConf.GetValues());
                    continue;
                }
                if (numConf != null)
                {
                    filtros[i] = new Filtro(filterLabels[i], numConf.GetValues());
                    continue;
                }
                filtros[i] = new Filtro(filterLabels[i], new string[0]);     
            }
        }

        FiltrosSelecionados fs = new FiltrosSelecionados(filtros);
            
        return JsonUtility.ToJson(fs);
    }

    public void DebugAtributosSelecionados()
    {
        string json = GetFiltrosConfigurados();        
        Debug.Log(json);
    }
}

public class FiltrosSelecionados
{
    public Filtro[] fSelecionados;

    public FiltrosSelecionados(Filtro[] fs)
    {
        fSelecionados = fs;
    }
}

public class Filtro
{
    public string atributo;
    public string[] values;

    public Filtro (string nome, string[] valores){
        atributo = nome;
        values = valores;
    }
}