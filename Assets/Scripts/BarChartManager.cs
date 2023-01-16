using TMPro;
using UnityEngine;

//TODO: Adicionar label do atributo cor na cena;
//TODO: Adicionar BarChart com Eixo Z

public class BarChartManager : MonoBehaviour
{
    public Transform VariaveisVisuaisParent;
    public Material[] TemplateMaterials;

    public TextMeshPro ChartNameLabel;
    public TextMeshPro XAxisLabel;
    public TextMeshPro YAxisLabel;
    public TextMeshPro ZAxisLabel;

    private int QtdObjetos;

    private GameObject[] ElementosVisuais;

    private const int TAMANHO_EIXOX = 10;

    public void CriaSimpleBarChart(
        string[] eixoX,
        float[] eixoY,
        string[] cor,
        string labelEixoX,
        string labelEixoY,
        string labelCor,
        string chartName)
    {
        if(!Utils.ArraysSaoDoMesmoTamanho(eixoX, eixoY, cor))
        {
            Debug.LogError("Os parâmetros não são do mesmo tamanho. Verifique e tente novamente!");
            return;
        }

        QtdObjetos = eixoX.Length;

        float[] EixoXNormalizado = Utils.CalculaPosicaoBarras(QtdObjetos, TAMANHO_EIXOX);
        float[] EixoYNormalizado = Utils.NormalizaValoresComMultiplicador(eixoY, TAMANHO_EIXOX);
        int[] CorNormalizado = Utils.ConverteCategoriasParaNumerico(cor);

        ElementosVisuais = new GameObject[QtdObjetos];
        float espessura = Utils.CalculaEspessuraGameObject(QtdObjetos, TAMANHO_EIXOX);
        GameObject empty = new GameObject();

        for (int i = 0; i < QtdObjetos; i++)
        {
            //Instancia GO vazio
            ElementosVisuais[i] = Instantiate(original: empty,
                parent: VariaveisVisuaisParent,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity);
            ElementosVisuais[i].AddComponent<Barra>();
            ElementosVisuais[i].name = "Row " + i;

            //Define a localposition do objeto
            ElementosVisuais[i].transform.localPosition = new Vector3(
                EixoXNormalizado[i], EixoYNormalizado[i], 0);

            //Adiciona informacoes da base de dados ao objeto
            ElementosVisuais[i].GetComponent<Barra>().setAtributosBase(
                eixoX[i], eixoY[i], cor[i]); 

            //Cria barra com valores necessários pro Unity
            ElementosVisuais[i].GetComponent<Barra>().setAtributosGameObject(
               EixoXNormalizado[i], espessura, EixoYNormalizado[i], TemplateMaterials[CorNormalizado[i]]);

        }

        // Define label dos eixos
        XAxisLabel.text = labelEixoX;
        YAxisLabel.text = labelEixoY;

        if (chartName == "")
            ChartNameLabel.text = labelEixoX + " X " + labelEixoY + " X " + labelCor;
        else
            ChartNameLabel.text = chartName;
    }

    public void CriaSimpleBarChart(
        string[] eixoX,
        float[] eixoY,
        string labelEixoX,
        string labelEixoY,
        string chartName,
        Material cor
        )
    {
        if (!Utils.ArraysSaoDoMesmoTamanho(eixoX, eixoY))
        {
            Debug.LogError("Os parâmetros não são do mesmo tamanho. Verifique e tente novamente!");
            return;
        }

        QtdObjetos = eixoX.Length;

        float[] EixoXNormalizado = Utils.CalculaPosicaoBarras(QtdObjetos, TAMANHO_EIXOX);
        float[] EixoYNormalizado = Utils.NormalizaValoresComMultiplicador(eixoY, TAMANHO_EIXOX);

        ElementosVisuais = new GameObject[QtdObjetos];
        float espessura = Utils.CalculaEspessuraGameObject(QtdObjetos, TAMANHO_EIXOX);
        GameObject empty = new GameObject();

        for (int i = 0; i < QtdObjetos; i++)
        {
            //Instancia GO vazio
            ElementosVisuais[i] = Instantiate(original: empty,
                parent: VariaveisVisuaisParent,
                position: new Vector3(0, 0, 0),
                rotation: Quaternion.identity);
            ElementosVisuais[i].AddComponent<Barra>();
            ElementosVisuais[i].name = "Row " + i;

            //Define a localposition do objeto
            ElementosVisuais[i].transform.localPosition = new Vector3(
                EixoXNormalizado[i], EixoYNormalizado[i], 0);

            //Adiciona informacoes da base de dados ao objeto
            ElementosVisuais[i].GetComponent<Barra>().setAtributosBase(
                eixoX[i], eixoY[i]);

            //Cria barra com valores necessários pro Unity
            ElementosVisuais[i].GetComponent<Barra>().setAtributosGameObject(
               EixoXNormalizado[i], espessura, EixoYNormalizado[i], cor);

        }

        // Define label dos eixos
        XAxisLabel.text = labelEixoX;
        YAxisLabel.text = labelEixoY;

        if (chartName == "")
            ChartNameLabel.text = labelEixoX + " X " + labelEixoY;
        else
            ChartNameLabel.text = chartName;
    }


    // remove on deploy
    void Start()
    {
        string[] X = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };
        float[] Y = new float[] { 6.3F, 2.5F, 3.8F, 7.5F, 4.3F, 1.9F, 9.2F, 8.2F, 8.4F, 4.9F, 9.2F, 3.5F, 2.3F, 7.0F, 6.6F };

        string[] COR = new string[] {"azul", "vermelho", "verde", "laranja",
            "azul", "marrom", "marrom", "preto", "vermelho", "lilas",
            "vermelho", "laranja", "preto", "branco", "preto"
        };


        CriaSimpleBarChart(X, Y, COR, "Marca", "Custo", "País", "Custo por Marca e País");
    }
}
