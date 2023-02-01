using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumericFilterConfiguration : MonoBehaviour
{
    private float min;
    private float max;
    private bool inverter;

    public TextMeshProUGUI helper;

    public TMP_InputField inputMinValue; 
    public TMP_InputField inputMaxValue;

    public TMP_Text inputMinPlaceholder;
    public TMP_Text inputMaxPlaceholder;

    public Toggle selecaoInvertida;

    //TODO: Add validacao de valores adicionados pelo usuario

    public void SetOptions(Vector2 valorMinMax)
    {
        min = valorMinMax.x;
        max = valorMinMax.y;

        inputMinPlaceholder.text = min.ToString();
        inputMaxPlaceholder.text = max.ToString();

        helper.text = $"Both value must be between " + min + " and " + max;
    }

    public string[] GetValues() // TODO: Adicionar informaçao do checkbox valores invertidos
    {
        return new string[] { inputMinValue.text, inputMaxValue.text, selecaoInvertida.isOn.ToString() };
    }

    public void ValidaValorInput()
    {
        float input1value = 0;
        float input2value = 0;

        float.TryParse(inputMinValue.text, out input1value);
        float.TryParse(inputMaxValue.text, out input2value);

        Debug.Log(input1value + "" + input2value);

        if (input1value < min) inputMinValue.text = min.ToString();
        if (input1value > max) inputMinValue.text = max.ToString();
        if (input2value < min) inputMaxValue.text = min.ToString();
        if (input2value > max) inputMaxValue.text = max.ToString();
    }
}
