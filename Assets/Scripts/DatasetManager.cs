using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{
    static private Dataset dataset;

    public static void SetDataset(string json)
    {
        dataset = Utils.CriaDoJSON(json);

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

        List<string> categoricLabels = new List<string>();
        List<string> numericLabels = new List<string>();

        for (int i=0; i < dataset.meta.Length; i++)
        {
            var column = dataset.meta[i];

            if (column.type.Equals("categorical"))
            {
                categoricLabels.Add(column.name);
                fw.SetAtributosCategoricos(i, column.extent);
            }
            if (column.type.Equals("numeric"))
            {
                numericLabels.Add(column.name);
                float min = 0;
                float max = 0;

                float.TryParse(column.extent[0], out min);
                float.TryParse(column.extent[1], out max);

                Vector2 fv = new Vector2(min, max);
                fw.SetRangeNumerico(i, fv);
            }
        }

        aw.SetLabelsAtributoCor(dataset.meta.Select(i => i.name.ToString()).ToArray());
        aw.SetLabelsAtributoEixoX(categoricLabels.ToArray());
        aw.SetLabelsAtributoEixoY(numericLabels.ToArray());



        /*
         
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
         */







    }


}
