using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI TextElement;

    public void Change(int Point)
    {
        TextElement.text = Point.ToString();
    }
}
