using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightBarBehavior : MonoBehaviour
{
    public TextMesh _low;
    public TextMesh _mid;
    public TextMesh _high;

    public void DefineTextoEixoY(Vector3 values)
    {
        _low.text = values.x.ToString();
        _mid.text = values.y.ToString();
        _high.text = values.z.ToString();
    }
}
