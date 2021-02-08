using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCircleData", menuName = "CircleData")]
public class CircleData : ScriptableObject {
    public float maxRadius = 1f;
    public float meanTime = 1f;
    public float stdDevTime = 0.25f;
    public float hideProbability = 0.25f;
    public float centerMultiplier = 6f;
    public float thickness = 1f;
}
