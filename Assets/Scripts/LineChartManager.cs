using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineChartManager : MonoBehaviour
{
    public Transform VariaveisVisuaisParent;
    public Material[] TemplateMaterials;

    public TextMeshPro XAxisLabel;
    public TextMeshPro YAxisLabel;
    //public TextMeshPro ZAxisLabel;

    public const int TAMANHO_EIXOX = 10;

    private GameObject[] ElementosVisuais;

    private int QtdObjetos;


    public void CriaSingleLineChart(
        string[] eixoX,
        float[] eixoY,
        //string[] cor,
        string labelEixoX,
        string labelEixoY,
        string labelCor)
    {
        if (!Utils.ArraysSaoDoMesmoTamanho(eixoX, eixoY))
        {
            Debug.LogError("Os parâmetros não são do mesmo tamanho. Verifique e tente novamente!");
            return;
        }


        QtdObjetos = eixoX.Length;
        ElementosVisuais = new GameObject[QtdObjetos];
        float[] EixoXNormalizado = Utils.CalculaPosicaoBarras(QtdObjetos, TAMANHO_EIXOX);
        float[] EixoYNormalizado = Utils.NormalizaValoresComMultiplicador(eixoY, TAMANHO_EIXOX);
        
        
        //int[] CorNormalizado = Utils.ConverteCategoriasParaNumerico(cor);

        ElementosVisuais = new GameObject[QtdObjetos];
        float tamanhoPonto = Utils.CalculaEspessuraGameObject(QtdObjetos, TAMANHO_EIXOX);

        GameObject empty = new GameObject();

        for (int i = 0; i < QtdObjetos; i++)
        {
            ElementosVisuais[i] = Instantiate(original: empty,
                parent: VariaveisVisuaisParent,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity
            );

            ElementosVisuais[i].name = "Row " + i;
            
            ElementosVisuais[i].AddComponent<PontoLinha>();
            ElementosVisuais[i].GetComponent<PontoLinha>().setAtributosBase(eixoX[i], eixoY[i]);

            ElementosVisuais[i].GetComponent<PontoLinha>().setAtributosGameObject(
                EixoXNormalizado[i], EixoYNormalizado[i]);

            ElementosVisuais[i].GetComponent<PontoLinha>().CriaPonto(TemplateMaterials[0]);


            if (i != QtdObjetos -1)
                ElementosVisuais[i].GetComponent<PontoLinha>().CriaLinha(TemplateMaterials[0]);

            ElementosVisuais[i].GetComponent<PontoLinha>().setTamanhoPonto(tamanhoPonto);

            if (i != 0)
            {
                ElementosVisuais[i - 1].GetComponent<PontoLinha>().setProximoPonto(ElementosVisuais[i]);
                ElementosVisuais[i - 1].GetComponent<PontoLinha>().ConectaProximoPonto();
            }
        }

        XAxisLabel.text = labelEixoX;
        YAxisLabel.text = labelEixoY;
        //ZAxisLabel.text = labelEixoZ;

        Destroy(empty);
    }


    

    // Start is called before the first frame update
    void Start()
    {
        string[] X = new string[] { "azul", "vermelho", "verde", "laranja",
            "azul", "marrom", "marrom", "preto", "vermelho", "lilas" };

        float[] Y = new float[] { 2, 3, 4, 3, 4, 5, 2, 3, 4, 2 };

        //string[] COR = new string[] {"azul", "vermelho", "verde", "laranja",
        //"azul", "marrom", "marrom", "preto", "vermelho", "lilas"
        //};

        CriaSingleLineChart(X, Y, "Producao", "Custo", "País");

        Vector2[][] matriz = new Vector2[2][];

        matriz[0] = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 3) };



    }


    

}
