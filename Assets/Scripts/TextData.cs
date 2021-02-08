using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTextData", menuName = "TextData")]
public class TextData : ScriptableObject
{
    [Range(2f, 64f)]
    public float fontSize = 24f;

    [Range(0.0625f, 10f)]
    public float fadeSpeed = 1f;
    public float yRange = 8;
    public float xRange = 12f;

    [SerializeField]
    public List<Color> colors = new List<Color>();


    [Delayed()]
    public string text = "Halfflame...";
}
