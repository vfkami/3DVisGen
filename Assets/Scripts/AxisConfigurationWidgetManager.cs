using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AxisConfigurationWidgetManager : MonoBehaviour
{
    public TMP_Dropdown xAxisSelector;
    public TMP_Dropdown yAxisSelector;
    public TMP_Dropdown colorSelector;

    private AtributosSelecionados atributos;

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
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        newOptions.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            newOptions.Add(option);
        }

        yAxisSelector.ClearOptions();
        yAxisSelector.AddOptions(newOptions);
    }

    public void SetLabelsAtributoCor(string[] labels)
    {
        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        newOptions.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            newOptions.Add(option);
        }

        colorSelector.ClearOptions();
        colorSelector.AddOptions(newOptions);
    }

    public string GetAtributosSelecionados()
    {
        string eixoX = GetAtributoSelecionadoEixoX();
        string eixoY = GetAtributoSelecionadoEixoY();
        string cor = GetAtributoSelecionadoCor();

        atributos = new AtributosSelecionados(eixoX, eixoY, cor);
        return JsonUtility.ToJson(atributos);
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
        if (value.Contains("Selecione") || value == "") return "null";

        return value;
    }

    public string GetAtributoSelecionadoCor()
    {
        string value = colorSelector.options[colorSelector.value].text;
        if (value.Contains("Selecione") || value == "") return "null";

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
        this.eixoX = x;
        this.eixoY = y;
        this.cor = c;
    }
}