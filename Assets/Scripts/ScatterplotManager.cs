using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ScatterplotManager : MonoBehaviour
{
    public GameObject[] TemplatePrefabs;
    public Material[] TemplateMaterials;
    public Transform VariaveisVisuaisParent;

    public TextMeshPro XAxisLabel;
    public TextMeshPro YAxisLabel;
    public TextMeshPro ZAxisLabel;

    public const int TAMANHO_EIXOX = 10;

    private GameObject[] ElementosVisuais;
    private int QtdObjetos;


    // TODO: Adicionar validacao tamanho array prefabs/material com categorias recebidas grupo/cor
    // TODO: Adicionar Labels de cor/grupo
    // TODO: Adicionar interacao ao clicar com mouse no elemento

    public void CriaScatterplot(
        float[] eixoX, 
        float[] eixoY, 
        float[] eixoZ, 
        string[] cor, 
        string[] grupo,
        string labelEixoX,
        string labelEixoY,
        string labelEixoZ,
        string labelCor,
        string labelGrupo)
    {
        if (!Utils.ArraysSaoDoMesmoTamanho(eixoX, eixoY, eixoZ, cor, grupo))
        {
            Debug.LogError("Os parâmetros não são do mesmo tamanho. Verifique e tente novamente!");
            return;
        }

        QtdObjetos = eixoX.Length;
        ElementosVisuais = new GameObject[QtdObjetos];

        float[] EixoXNormalizado = Utils.NormalizaValoresComMultiplicador(eixoX, TAMANHO_EIXOX);
        float[] EixoYNormalizado = Utils.NormalizaValoresComMultiplicador(eixoY, TAMANHO_EIXOX);
        float[] EixoZNormalizado = Utils.NormalizaValoresComMultiplicador(eixoZ, TAMANHO_EIXOX);
        int[] CorNormalizado = Utils.ConverteCategoriasParaNumerico(cor);
        int[] GrupoNormalizado = Utils.ConverteCategoriasParaNumerico(grupo);

        GameObject empty = new GameObject();

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


        Destroy(empty);

    }

    private void Start()
    {
        float[] X = new float[] { 5, 8, 3, 23, 41, 10, 49, 85, 100, 84, 91, 13, 32, 09, 84 };
        float[] Y = new float[] { 9, 2, 3, 7, 4, 1, 9, 8, 8, 4, 9, 3, 2, 7, 6 };
        float[] Z = new float[] { 6.3F, 2.5F, 3.8F, 7.5F, 4.3F, 1.9F, 9.2F, 8.2F, 8.4F, 4.9F, 9.2F, 3.5F, 2.3F, 7.0F, 6.6F };

        string[] COR = new string[] {"azul", "vermelho", "verde", "laranja", 
            "azul", "marrom", "marrom", "preto", "vermelho", "lilas",
            "vermelho", "laranja", "preto", "branco", "preto"
        };

        string[] GRUPO = new string[] {"a", "h", "c", "d", "g", "f", "c", 
            "e", "f", "b", "a", "f", "e", "b", "h"
        };

        CriaScatterplot(X, Y, Z, COR, GRUPO, "Pontuação", "Desempenho", "Custo", "Marca", "País");

    }
    

}
