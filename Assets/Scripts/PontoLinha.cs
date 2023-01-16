using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoLinha : MonoBehaviour
{
    //variáveis visuais:
    public float posX, posY, posZ;
    public Material corPrefab;
    public GameObject referenciaPrefabLinha;
    public GameObject referenciaPrefabPonto;


    //variáveis da base:
    public float yValor, zValor;
    public string xValor, grupoValor, corValor;

    public GameObject linhaParent;
    public GameObject linha;
    public GameObject ponto;

    public GameObject proximoPonto;

    public void setAtributosGameObject(float x, float y)
    {
        posX = x;
        posY = y;
        transform.localPosition = new Vector3(x, y, 0);
        return;
    }

    public void CriaLinha(Material cor)
    {
        referenciaPrefabLinha = GameObject.CreatePrimitive(PrimitiveType.Cube);

        

        linhaParent = new GameObject("linhaParent");
        linhaParent.transform.SetParent(this.transform);

        linha = Instantiate(
            original: referenciaPrefabLinha,
            parent: linhaParent.transform,
            position: Vector3.zero,
            rotation: Quaternion.identity
        );
        linhaParent.transform.localPosition = Vector3.zero;


        linha.transform.localPosition = new Vector3(0, 0.5F, 0);

        linha.name = "Linha";
        linha.GetComponent<Renderer>().material = cor;

        Destroy(referenciaPrefabLinha);
    }

    public void CriaPonto(Material cor)
    {
        referenciaPrefabPonto = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ponto = Instantiate(
            original: referenciaPrefabPonto,
            parent: this.transform,
            position: Vector3.zero,
            rotation: Quaternion.identity
        );

        ponto.name = "Ponto";
        ponto.GetComponent<Renderer>().material = cor;
        ponto.transform.localPosition = Vector3.zero;
        Destroy(referenciaPrefabPonto);
    }

    public void setCorGameObject(Material cor)
    {
        GetComponent<Renderer>().material = cor;
        
        corPrefab = cor;
        return;
    }

    //TODO: criar setAtributosGameObject com eixo z

    public void setTamanhoPonto(float size)
    {
        ponto.transform.localScale = new Vector3(size, size, size);
    }

    public void setAtributosBase(string x, float y, float z, string grupo, string cor)
    {
        xValor = x;
        yValor = y;
        zValor = z;
        grupoValor = grupo;
        corValor = cor;
    }

    public void setAtributosBase(string x, float y, string cor)
    {
        xValor = x;
        yValor = y;
        corValor = cor;
    }

    public void setAtributosBase(string x, float y)
    {
        xValor = x;
        yValor = y;
    }

    public GameObject getLinhaParentVariavelVisual()
    {
        return linhaParent;
    }

    public void setProximoPonto(GameObject go)
    {
        proximoPonto = go;
    }

    public void ConectaProximoPonto()
    {
        Utils.ConectaDoisPontos(gameObject, proximoPonto);
    }

    public void ResetaPosicaoLinhaePonto()
    {
        linhaParent.transform.localPosition = Vector3.zero;
        ponto.transform.localPosition = Vector3.zero;
        linha.transform.localScale = new Vector3(0.5F, 1F, 0.5F);

    }
}
