using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumericFilterConfiguration : MonoBehaviour
{
    private float _min;
    private float _max;
    private bool _inverter;

    public TextMeshProUGUI helper;
    public TMP_InputField inputMinValue; 
    public TMP_InputField inputMaxValue;
    public TMP_Text inputMinPlaceholder;
    public TMP_Text inputMaxPlaceholder;
    public Toggle selecaoInvertida;

    public void SetRange(Vector2 valorMinMax)
    {
        _min = valorMinMax.x;
        _max = valorMinMax.y;

        inputMinPlaceholder.text = _min.ToString();
        inputMaxPlaceholder.text = _max.ToString();

        helper.text = $"Both value must be between " + _min + " and " + _max;
    }

    public string[] GetValores()
    {
        return new string[] { inputMinValue.text, inputMaxValue.text, selecaoInvertida.isOn.ToString() };
    }

    public void ValidaValorInput()
    {
        float input1value;
        float input2value;

        float.TryParse(inputMinValue.text, out input1value);
        float.TryParse(inputMaxValue.text, out input2value);

        if (input1value < _min) inputMinValue.text = _min.ToString();
        if (input1value > _max) inputMinValue.text = _max.ToString();
        if (input2value < _min) inputMaxValue.text = _min.ToString();
        if (input2value > _max) inputMaxValue.text = _max.ToString();
    }
}
