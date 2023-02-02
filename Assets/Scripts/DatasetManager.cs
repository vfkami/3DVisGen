using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{
    static private Dataset _dataset;

    public static void SetDataset(string json)
    {
        try
        {
            _dataset = Utils.CriaDoJSON(json);
        }
        catch (ArgumentException ex)
        {
            _dataset = new Dataset()
            {
                columns = new string[0],
                rows = 0,
                meta = new Metadata[0]
            };

            Debug.LogError(ex.Message);
        }

        AtualizaTodosElementosCanvas();
    }

    public static void AtualizaTodosElementosCanvas()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        AxisConfigurationWidgetManager axisWidget =
            canvas.GetComponentInChildren<AxisConfigurationWidgetManager>();
        FilterConfigurationWidgetManager filterWidget =
            canvas.GetComponentInChildren<FilterConfigurationWidgetManager>();
        
        filterWidget.SetLabelsFiltro(_dataset.meta
            .Select(i => i.name.ToString()).ToArray());

        filterWidget.SetTipoFiltros(_dataset.meta
            .Select(i => i.type.ToString()).ToArray());

        filterWidget.SetInfoFiltros(_dataset.meta
            .Select(i => i.extent.ToArray<string>()).ToArray());

        List<string> categoricLabels = new List<string>();
        List<string> numericLabels = new List<string>();


        for (int i=0; i < _dataset.meta.Length; i++)
        {
            var column = _dataset.meta[i];

            if (column.type.Equals("categorical"))
            {
                categoricLabels.Add(column.name);
            }
            if (column.type.Equals("numeric"))
            {
                numericLabels.Add(column.name);
            }
        }

        axisWidget.SetLabelsAtributoCor(
            _dataset.meta.Select(i => i.name.ToString()).ToArray());

        axisWidget.SetLabelsAtributoEixoX(categoricLabels.ToArray());

        axisWidget.SetLabelsAtributoEixoY(numericLabels.ToArray());
    }


}

[System.Serializable]
public class Dataset
{
    public string[] columns;
    public int rows;
    public Metadata[] meta;
}

[System.Serializable]
public class Metadata
{
    public string name;
    public string type;
    public string[] extent;
}
