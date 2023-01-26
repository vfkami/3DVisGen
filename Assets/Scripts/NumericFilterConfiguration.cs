using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumericFilterConfiguration : MonoBehaviour
{
    public TextMeshProUGUI helper;

    public TMP_InputField inputMinValue; 
    public TMP_InputField inputMaxValue;

    public TMP_Text inputMinPlaceholder;
    public TMP_Text inputMaxPlaceholder;

    //TODO: Add validacao de valores adicionados pelo usuario

    public void SetOptions(Vector2 valorMinMax)
    {
        inputMinPlaceholder.text = valorMinMax.x.ToString();
        inputMaxPlaceholder.text = valorMinMax.y.ToString();

        helper.text = $"Both value must be between " + valorMinMax.x + " and " + valorMinMax.y;
    }

    public string[] GetValues() // TODO: Adicionar informaçao do checkbox valores invertidos
    {
        return new string[] { inputMinValue.text, inputMaxValue.text };
    }

}
