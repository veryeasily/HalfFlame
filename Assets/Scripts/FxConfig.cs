using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFxConfig", menuName = "FxConfig")]
public class FxConfig : ScriptableObject {
    public float maxRadius = 1f;
    public float meanTime = 1f;
    public float stdDevTime = 0.25f;
    public float hideProbability = 0.25f;
    public float centerMultiplier = 6f;
    public float thickness = 1f;
    
    [Range(2f, 64f)] public float fontSize = 24f;
    [Range(0.0625f, 10f)] public float fadeSpeed = 1f;
    public float yRange = 8;
    public float xRange = 12f;
    [SerializeField] public List<Color> colors = new List<Color>();
    [Delayed()] public string text = "Ummm...";
}