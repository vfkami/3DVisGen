using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxisConfigurationWidgetManager : MonoBehaviour
{
    public TMP_Dropdown xAxisSelector;
    public TMP_Dropdown yAxisSelector;
    public TMP_Dropdown colorSelector;

    private AtributosSelecionados _atributos;

    public void SetLabelsAtributoEixoX(string[] labels)
    {
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        newOptions.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            newOptions.Add(option);
        }

        xAxisSelector.ClearOptions();
        xAxisSelector.AddOptions(newOptions);

    }


    public void SetLabelsAtributoEixoY(string[] labels)
    {
        List<TMP_Dropdown.OptionData> opcoes = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData opcao = new TMP_Dropdown.OptionData("Selecione");
        opcoes.Add(opcao);

        foreach (string label in labels)
        {
            opcao = new TMP_Dropdown.OptionData(label);
            opcoes.Add(opcao);
        }

        yAxisSelector.ClearOptions();
        yAxisSelector.AddOptions(opcoes);
    }

    public void SetLabelsAtributoCor(string[] labels)
    {
        List<TMP_Dropdown.OptionData> opcoes = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        opcoes.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            opcoes.Add(option);
        }

        colorSelector.ClearOptions();
        colorSelector.AddOptions(opcoes);
    }

    public string GetAtributosSelecionados()
    {
        string eixoX = GetAtributoSelecionadoEixoX();
        string eixoY = GetAtributoSelecionadoEixoY();
        string cor = GetAtributoSelecionadoCor();

        _atributos = new AtributosSelecionados(eixoX, eixoY, cor);
        return JsonUtility.ToJson(_atributos);
    }
    
    public void DebugAtributosSelecionados()
    {
        string json = GetAtributosSelecionados();
        Debug.Log(json);
    }

    public string GetAtributoSelecionadoEixoX()
    {
        string value = xAxisSelector.options[xAxisSelector.value].text;
        if (value.Contains("Selecione") || value == "" ) return "null";

        return value;
    }

    public string GetAtributoSelecionadoEixoY()
    {
        string value = yAxisSelector.options[yAxisSelector.value].text;
        if (value.Equals("Selecione") || value == "") return "null";

        return value;
    }

    public string GetAtributoSelecionadoCor()
    {
        string value = colorSelector.options[colorSelector.value].text;
        if (value.Equals("Selecione") || value == "") return "null";

        return value;
    }
}

class AtributosSelecionados 
{
    public string eixoX;
    public string eixoY;
    public string cor;

    public AtributosSelecionados(string x, string y, string c)
    {
        eixoX = x;
        eixoY = y;
        cor = c;
    }
}