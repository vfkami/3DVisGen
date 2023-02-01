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

    public TMP_Dropdown filtroSelector;

    public GameObject ancoraFiltros;
    public GameObject CategoricTemplate;
    public GameObject NumericTemplate;

    private Filtro[] filtros;

    public void SetLabelsFiltro(string[] labels)
    {
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


    public void SetAtributosCategoricos(int index, string[] options)
    {
        if (!filterType[index].Equals(CATEGORIC)) 
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return;

        }
    }

    public void SetRangeNumerico(int index, Vector2 valorMinMax)
    {
        if (!filterType[index].Equals(NUMERIC))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return;
        }
    }


    public void DesabilitarVisibilidadeTodosFiltros()
    {
        foreach (GameObject filter in filters)
            if (filter != null)    
                filter.SetActive(false);
    }

    public void OnFiltroSelectorValueChanged()
    {
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

                filters[index].transform.localPosition = Vector3.zero;

                //get das informações para preencher este filtro
                string[] abc = new string[] { "abc", "def", "ghi", "jkl" };

                filters[index].GetComponent<CategoricFilterConfiguration>().SetOptions(abc);
            }
            else
            {
                filters[index] = Instantiate(original: NumericTemplate,
                parent: ancoraFiltros.transform,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
                );
                
                filters[index].transform.localPosition = Vector3.zero;

                //get das informações para preencher este filtro
                Vector2 minMax = new Vector2(10, 200);
                filters[index].GetComponent<NumericFilterConfiguration>().SetOptions(minMax);
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

    private void Start()
    {
        string[] labelFiltros = new string[] {"Pais", "Cor", "Potencia", "Preço" };
        string[] tipoFiltros = new string[] {"categoric", "categoric", "numeric", "numeric"};

        string[] atributo0values = new string[] { "Brasil", "Argentina", "Uruguai" };
        string[] atributo1values = new string[] { "Azul", "Verde", "Amarelo", "Vermelho" };
        Vector2 atributo2values = new Vector2(500F, 2750F);
        Vector2 atributo3values = new Vector2(50F, 500F);

        //SetLabelsFiltro(labelFiltros);
        //SetTipoFiltros(tipoFiltros);
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