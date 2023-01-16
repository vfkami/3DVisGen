using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static float[] NormalizaValores(float[] valores)
    {
        float[] valoresNormalizados = new float[valores.Length];

        for (int i = 0; i < valores.Length; i++)
            valoresNormalizados[i] = (valores[i] - valores.Min()) / (valores.Max() - valores.Min()); 
            
        return valoresNormalizados;
    }

    public static float[] NormalizaValoresComMultiplicador(float[] valores, int multiplicador)
    {
        float[] valoresNormalizados = new float[valores.Length];

        for (int i = 0; i < valores.Length; i++)
            valoresNormalizados[i] = ((valores[i] - valores.Min()) / (valores.Max() - valores.Min())) * multiplicador;

        return valoresNormalizados;
    }

    public static int[] ConverteCategoriasParaNumerico(string[] valores)
    {
        int count = 0;
        int[] valoresNormalizados = new int[valores.Length];
        Dictionary<string, int> dicionario = new Dictionary<string, int>();

        for (int i = 0; i < valores.Length; i++)
        {
            if (!dicionario.ContainsKey(valores[i]))
            {
                dicionario.Add(valores[i], count);
                valoresNormalizados[i] = count;
                count++;
            }
            else
                valoresNormalizados[i] = dicionario[valores[i]];
        }

        return valoresNormalizados;
    }

    public static bool ArraysSaoDoMesmoTamanho(params Array[] arrays)
    {
        return arrays.All(a => a.Length == arrays[0].Length);
    }


    public static float[] CalculaPosicaoBarras(int qtdBarras, int tamanhoEixoX)
    {
        float espacamento = CalculaEspacamentoEntreBarras(qtdBarras, tamanhoEixoX);
        float grossuraBarra = CalculaEspessuraGameObject(qtdBarras, tamanhoEixoX);

        float espacoTotalBarra = grossuraBarra + espacamento;
        float[] posicaoBarras = new float[qtdBarras];

        for (int i = 0; i < qtdBarras; i++) {
            posicaoBarras[i] = espacoTotalBarra * i;
        }

        return posicaoBarras;
    }

    public static float CalculaEspessuraGameObject(int qtdBarras, int tamanhoEixoX)
    {
        float decimo = tamanhoEixoX / 10;
        float espacoTotal = tamanhoEixoX - decimo;
        return espacoTotal / qtdBarras;
    }

    public static float CalculaEspacamentoEntreBarras(int qtdBarras, int tamanhoEixoX)
    {
        float decimo = tamanhoEixoX / 10;
        return decimo / qtdBarras; 
    }

    // Calcula o ângulo de rotação para que um objeto X aponte para um objeto Y. Retorna o ângulo de rotação;
    public static Quaternion CalculaAnguloEntreDoisPontos(GameObject x, GameObject y)
    {
        // i'll assume that dots are in the same z axis 
        Vector3 direction = CalculaDirecaoEntreDoisPontos(x, y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        return angleAxis;
    }

    public static Vector3 CalculaDirecaoEntreDoisPontos(GameObject x, GameObject y)
    {
        return y.transform.position - x.transform.position;
    }

    // Limitation: Only rotate in X angle; 
    public static void RotacionaObjeto(GameObject x, Quaternion angle)
    {
        x.transform.rotation = Quaternion.Slerp(x.transform.rotation, angle, Time.deltaTime * 50);

        return;
    }

    public static void EscalaLinhaParaTocarSegundoPonto(GameObject x, GameObject y)
    {
        Vector3 direction = CalculaDirecaoEntreDoisPontos(x, y);

        //The size of line will be the hipotenuse of 3d triangle: 
        //See tutorial here: https://www.mathsisfun.com/geometry/pythagoras-3d.html
        var hipotenuse = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2) + Mathf.Pow(direction.z, 2));
        var linhaParent = x.GetComponent<PontoLinha>().getLinhaParentVariavelVisual();

        linhaParent.transform.localScale = new Vector3(1, hipotenuse, 1);

        return;
    }

    public static void ConectaDoisPontos(GameObject x, GameObject y)
    {
        Quaternion angulo = CalculaAnguloEntreDoisPontos(x, y);
        RotacionaObjeto(x, angulo);
        EscalaLinhaParaTocarSegundoPonto(x, y);

        return;
    }



}
