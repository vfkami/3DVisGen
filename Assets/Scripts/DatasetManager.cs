using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{
    static private Dataset dataset;

    public static void SetDataset(string json)
    {
        try
        {
            dataset = Utils.CriaDoJSON(json);
        }
        catch (ArgumentException ex)
        {
            dataset = new Dataset()
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
        Canvas cv = GameObject.Find("Canvas").GetComponent<Canvas>();

        AxisConfigurationWidgetManager aw =
            cv.GetComponentInChildren<AxisConfigurationWidgetManager>();
        FilterConfigurationWidgetManager fw =
            cv.GetComponentInChildren<FilterConfigurationWidgetManager>();
        
        fw.SetLabelsFiltro(dataset.meta.Select(i => i.name.ToString()).ToArray());
        fw.SetTipoFiltros(dataset.meta.Select(i => i.type.ToString()).ToArray());
        fw.SetInfoFiltros(dataset.meta.Select(i => i.extent.ToArray<string>()).ToArray());

        List<string> categoricLabels = new List<string>();
        List<string> numericLabels = new List<string>();


        for (int i=0; i < dataset.meta.Length; i++)
        {
            var column = dataset.meta[i];

            if (column.type.Equals("categorical"))
            {
                categoricLabels.Add(column.name);
            }
            if (column.type.Equals("numeric"))
            {
                numericLabels.Add(column.name);
            }
        }

        aw.SetLabelsAtributoCor(dataset.meta.Select(i => i.name.ToString()).ToArray());
        aw.SetLabelsAtributoEixoX(categoricLabels.ToArray());
        aw.SetLabelsAtributoEixoY(numericLabels.ToArray());
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
