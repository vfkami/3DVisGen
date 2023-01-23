using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoricFilterConfiguration : MonoBehaviour
{
    public Toggle[] goToggle;
    private string[] values;

    public void SetOptions(string[] labelText)
    {
        values = labelText;

        if (labelText.Length > goToggle.Length)
        {
            Debug.LogWarning(
                $"A quantidade de atributos excede o máximo aceitado. Só serão exibidas 10 opções.");
        }

        for(int i=0; i < goToggle.Length; i++)
        {
            if (i < labelText.Length)
                goToggle[i].GetComponentInChildren<TextMeshProUGUI>().text = labelText[i];
            else
                goToggle[i].gameObject.SetActive(false);
        }
    }

    public string[] GetValues()
    {
        List<string> valoresSelecionados = new List<string>();

        for(int i = 0; i < goToggle.Length; i++)
            if (goToggle[i].isOn)
                valoresSelecionados.Add(values[i]);

        return valoresSelecionados.ToArray();
    }
}
