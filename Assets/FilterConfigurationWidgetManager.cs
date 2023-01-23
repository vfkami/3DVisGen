using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using System.Linq;

public class FilterConfigurationWidgetManager : MonoBehaviour
{
    private GameObject[] filters;
    private string[] filterType;

    public TMP_Dropdown filtroSelector;

    public GameObject ancoraFiltros;

    public GameObject CategoricTemplate;
    public GameObject NumericTemplate;

    public void SetLabelsFiltro(string[] labels)
    {
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
        if (!filterType[index].Contains("categoric"))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return;
        }
    }

    public void SetRangeNumerico(int index, Vector2 valorMinMax)
    {
        if (!filterType[index].Contains("numeric"))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return;
        }
    }

    public void OnFiltroSelectorValueChanged()
    {

    }


    private void Start()
    {
        string[] labelFiltros = new string[] {"Pais", "Cor", "Potencia", "Preço" };
        string[] tipoFiltros = new string[] {"categoric", "categoric", "numeric", "numeric"};

        string[] atributo0values = new string[] { "Brasil", "Argentina", "Uruguai" };
        string[] atributo1values = new string[] { "Azul", "Verde", "Amarelo", "Vermelho" };
        Vector2 atributo2values = new Vector2(500F, 2750F);
        Vector2 atributo3values = new Vector2(50F, 500F);

        SetLabelsFiltro(labelFiltros);
        SetTipoFiltros(tipoFiltros);
    }

}
