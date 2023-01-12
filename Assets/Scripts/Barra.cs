using TMPro;
using UnityEngine;

public class Barra : MonoBehaviour
{
    //variáveis visuais:
    public float posX, posY, posZ;
    public Material corPrefab;
    public GameObject referenciaPrefab;

    //variáveis da base:
    public float yValor, zValor;
    public string xValor, grupoValor, corValor;

    public void setAtributosGameObject(float x, float grossuraBarra, float y, Material cor)
    {
        posX = x;
        posY = y;
        corPrefab = cor;

        referenciaPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);

        float alturaMinima = 0.5F;

        GameObject variavelVisual = Instantiate(
            original: referenciaPrefab,
            parent: this.transform,
            position: new Vector3(0, 0, 0),
            rotation: Quaternion.identity
        ) ;

        variavelVisual.transform.localPosition = new Vector3(0.5F, 0.5F, 0);
        variavelVisual.GetComponent<Renderer>().material = cor;
        variavelVisual.name = "VariavelVisual";

        transform.localPosition = new Vector3(x, 0, 0);
        transform.localScale = new Vector3(grossuraBarra, y + alturaMinima, 1);

        Destroy(referenciaPrefab);
        
        return;
    }

    //TODO: criar setAtributosGameObject com eixo z

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

}
