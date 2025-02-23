using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private static TextMeshProUGUI text_static;

    private void Awake()
    {
        // Store static copy
        text_static = text;
    }

    /// <summary>
    /// Static function for printing to the Console prefab
    /// </summary>
    /// <param name="text"></param>
    public static void Log(string text)
    {
        text_static.text = text;
    }
}
