using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
	/*
        {
	"columns": [
		"sepal_length",
		"sepal_width",
		"petal_length",
		"petal_width",
		"iris"
	],
	"rows": 150,
	"meta": [{
			"name": "sepal_length",
			"type": "numeric",
			"extent": [
				4.3,
				7.9
			]
		},
		{
			"name": "sepal_width",
			"type": "numeric",
			"extent": [
				2,
				4.4
			]
		},
		{
			"name": "petal_length",
			"type": "numeric",
			"extent": [
				1,
				6.9
			]
		},
		{
			"name": "petal_width",
			"type": "numeric",
			"extent": [
				0.1,
				2.5
			]
		},
		{
			"name": "iris",
			"type": "categorical",
			"extent": [
				"Iris-setosa",
				"Iris-versicolor",
				"Iris-virginica"
			]
		}
	]
}
     */
	public string JSON;

	// Start is called before the first frame update
	void Start()
    {

        var metadados = CriaDoJSON(JSON);

        Debug.Log(JSON);
    }

    public static Dataset CriaDoJSON(string JSON)
    {
        return JsonUtility.FromJson<Dataset>(JSON);
    }

}

[System.Serializable]
public class Dataset
{
    public string[] columns;
    public int rows;
	public Metadata[] meta;
}

[System.Serializable]
public class Metadata
{
	public string name;
	public string type;
	public string[] extent;
}
