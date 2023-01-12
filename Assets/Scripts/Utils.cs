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
        float grossuraBarra = CalculaGrossuraBarra(qtdBarras, tamanhoEixoX);

        float espacoTotalBarra = grossuraBarra + espacamento;
        float[] posicaoBarras = new float[qtdBarras];

        for (int i = 0; i < qtdBarras; i++) {
            posicaoBarras[i] = espacoTotalBarra * i;
        }

        return posicaoBarras;
    }

    public static float CalculaGrossuraBarra(int qtdBarras, int tamanhoEixoX)
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

}
