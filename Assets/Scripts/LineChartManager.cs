using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineChartManager : MonoBehaviour
{
    public GameObject Ponto;
    public Material Cor;
    public Transform VariaveisVisuaisParent;

    public TextMeshPro XAxisLabel;
    public TextMeshPro YAxisLabel;
    //public TextMeshPro ZAxisLabel;

    public const int TAMANHO_EIXOX = 10;

    private GameObject[] ElementosVisuais;
    private int QtdObjetos;


    public void CriaLineChart(
        float[] eixoX,
        float[] eixoY,
        string[] cor,
        string[] grupo,
        string labelEixoX,
        string labelEixoY,
        string labelCor,
        string labelGrupo)
    {
        if (!Utils.ArraysSaoDoMesmoTamanho(eixoX, eixoY, cor, grupo))
        {
            Debug.LogError("Os parâmetros não são do mesmo tamanho. Verifique e tente novamente!");
            return;
        }

        QtdObjetos = eixoX.Length;
        ElementosVisuais = new GameObject[QtdObjetos];

        float[] EixoXNormalizado = Utils.NormalizaValoresComMultiplicador(eixoX, TAMANHO_EIXOX);
        float[] EixoYNormalizado = Utils.NormalizaValoresComMultiplicador(eixoY, TAMANHO_EIXOX);
        int[] CorNormalizado = Utils.ConverteCategoriasParaNumerico(cor);
        int[] GrupoNormalizado = Utils.ConverteCategoriasParaNumerico(grupo);

        GameObject empty = new GameObject();
        /*
        for (int i = 0; i < QtdObjetos; i++)
        {
            ElementosVisuais[i] = Instantiate(original: empty,
                parent: VariaveisVisuaisParent,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity);

            ElementosVisuais[i].transform.localPosition = new Vector3(
                EixoXNormalizado[i], EixoYNormalizado[i], EixoZNormalizado[i]);

            ElementosVisuais[i].AddComponent<VariavelVisual>();
            ElementosVisuais[i].GetComponent<VariavelVisual>().setAtributosBase(
                eixoX[i], eixoY[i], eixoZ[i], grupo[i], cor[i]);

            ElementosVisuais[i].GetComponent<VariavelVisual>().setAtributosGameObject(
                EixoXNormalizado[i], EixoYNormalizado[i], EixoZNormalizado[i],
                TemplatePrefabs[GrupoNormalizado[i]], TemplateMaterials[CorNormalizado[i]]);

            ElementosVisuais[i].name = "Row " + i;
        }

        XAxisLabel.text = labelEixoX;
        YAxisLabel.text = labelEixoY;
        ZAxisLabel.text = labelEixoZ;
        */

        Destroy(empty);

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }


    

}
