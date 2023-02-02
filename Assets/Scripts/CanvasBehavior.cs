using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject UIElements;

    public void LigarDesligarCanvas()
    {
        bool active = UIElements.gameObject.activeSelf;
        UIElements.gameObject.SetActive(!active);
    }
}
