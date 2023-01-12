using TMPro;
using UnityEngine;

public class BarChartManager : MonoBehaviour
{
    public Transform VariaveisVisuaisParent;
    public Material[] TemplateMaterials;

    public TextMeshPro XAxisLabel;
    public TextMeshPro YAxisLabel;
    public TextMeshPro ZAxisLabel;

    private int QtdObjetos;

    private GameObject[] ElementosVisuais;

    private const int TAMANHO_EIXOX = 10;

    // 3D Bar Chart sem eixo Z
    //TODO: Adicionar label do atributo cor na cena;
    public void CriaBarChart(
        string[] eixoX,
        float[] eixoY,
        string[] cor,
        string labelEixoX,
        string labelEixoY,
        string labelCor)
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
        float grossuraBarra = Utils.CalculaGrossuraBarra(QtdObjetos, TAMANHO_EIXOX);
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
               EixoXNormalizado[i], grossuraBarra, EixoYNormalizado[i], TemplateMaterials[CorNormalizado[i]]);

        }

        XAxisLabel.text = labelEixoX;
        YAxisLabel.text = labelEixoY;
    }

    //TODO: Adicionar BarChart com eixo Z

    // remove on deploy
    void Start()
    {
        string[] X = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };
        float[] Y = new float[] { 6.3F, 2.5F, 3.8F, 7.5F, 4.3F, 1.9F, 9.2F, 8.2F, 8.4F, 4.9F, 9.2F, 3.5F, 2.3F, 7.0F, 6.6F };

        string[] COR = new string[] {"azul", "vermelho", "verde", "laranja",
            "azul", "marrom", "marrom", "preto", "vermelho", "lilas",
            "vermelho", "laranja", "preto", "branco", "preto"
        };


        CriaBarChart(X, Y, COR, "Marca", "Custo", "País");
    }
}
