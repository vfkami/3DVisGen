using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;
using System.Linq;

public class FilterConfigurationWidgetManager : MonoBehaviour
{
    private static string Categoric = "categorical";
    private static string Numeric = "numeric";

    private GameObject[] _filtrosGameObject;
    private Filtro[] _filtros;
    private string[] _labelFiltros;
    private string[] _tipoFiltros;
    private string[][] _informacaoFiltros;
    
    public TMP_Dropdown dpdSeletorFiltros;

    public GameObject ancoraFiltros;
    public GameObject templateCategorico;
    public GameObject templateNumerico;


    public void SetLabelsFiltro(string[] labels)
    {
        if (_filtrosGameObject != null)
            _filtrosGameObject.Where(go => go != null).ToList()
                .ForEach(go => Destroy(go));

        _labelFiltros = labels;
        _filtrosGameObject = new GameObject[labels.Length];

        List<TMP_Dropdown.OptionData> newOptions = new List<TMP_Dropdown.OptionData>();
        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData("Selecione");
        newOptions.Add(option);

        foreach (string label in labels)
        {
            option = new TMP_Dropdown.OptionData(label);
            newOptions.Add(option);
        }

        dpdSeletorFiltros.ClearOptions();
        dpdSeletorFiltros.AddOptions(newOptions);
    }

    // accept only: categoric, numeric
    public void SetTipoFiltros(string[] tipos)
    {
        _tipoFiltros = tipos;
    }

    public void SetInfoFiltros(string[][] infos)
    {
        _informacaoFiltros = infos;
    }

    private string[] GetLabelAtributosCategoricos(int index)
    {
        if (!_tipoFiltros[index].Equals(Categoric))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return new string[0];

        }

        return _informacaoFiltros[index];
    }

    private Vector2 GetRangeNumerico(int index)
    {
        if (!_tipoFiltros[index].Equals(Numeric))
        {
            Debug.LogError(
                $"O atributo passado no índice " + index + " não corresponde a um atributo categórico");
            return new Vector2(0, 0);
        }

        var extent = _informacaoFiltros[index];

        float min = 0;
        float max = 0;

        float.TryParse(extent[0], out min);
        float.TryParse(extent[1], out max);

        return new Vector2(min, max);
    }

    public void OnFiltroSelectorValueChanged()
    {
        _filtrosGameObject.Where(go => go != null).ToList()
            .ForEach(go => go.SetActive(false));

        var index = dpdSeletorFiltros.value - 1;
        if (index < 0) return;

        if (_filtrosGameObject[index] == null)
        {        
            if (_tipoFiltros[index].Contains("categoric"))
            {
                _filtrosGameObject[index] = Instantiate(original: templateCategorico,
                parent: ancoraFiltros.transform,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
                );

                _filtrosGameObject[index].name = _labelFiltros[index] + "_cat_" + index;
                _filtrosGameObject[index].transform.localPosition = Vector3.zero;
                _filtrosGameObject[index].GetComponent<CategoricFilterConfiguration>().
                    SetOptions(GetLabelAtributosCategoricos(index));
            }
            else
            {
                _filtrosGameObject[index] = Instantiate(original: templateNumerico,
                parent: ancoraFiltros.transform,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
                );

                _filtrosGameObject[index].name = _labelFiltros[index] + "_num_" + index;
                _filtrosGameObject[index].transform.localPosition = Vector3.zero;
                _filtrosGameObject[index].GetComponent<NumericFilterConfiguration>().
                    SetRange(GetRangeNumerico(index));
            }
        }

        _filtrosGameObject[index].SetActive(true);
    }

    public string GetFiltrosConfigurados()
    {
        _filtros = new Filtro[_labelFiltros.Length];


        for(int i = 0; i < _labelFiltros.Length; i++)
        {
            CategoricFilterConfiguration catConf;
            NumericFilterConfiguration numConf;

            if(_filtrosGameObject[i] != null)
            {
                _filtrosGameObject[i].TryGetComponent<CategoricFilterConfiguration>(out catConf);
                _filtrosGameObject[i].TryGetComponent<NumericFilterConfiguration>(out numConf);

                if (catConf != null)
                {
                    _filtros[i] = new Filtro(_labelFiltros[i], catConf.GetValores());
                    continue;
                }
                if (numConf != null)
                {
                    _filtros[i] = new Filtro(_labelFiltros[i], numConf.GetValores());
                    continue;
                }
                _filtros[i] = new Filtro(_labelFiltros[i], new string[0]);     
            }
        }

        FiltrosSelecionados fs = new FiltrosSelecionados(_filtros);
            
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