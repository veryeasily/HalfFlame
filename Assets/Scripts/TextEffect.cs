using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TextEffect : EffectBehaviour {
    public float opacity = 0f;
    public float fontSizeMultiplier = 1f;
    public Color _color = Color.black;

    private int i = 0;
    private int counter = 0;
    private float currentFontSize = 0f;
    private Animator animator;
    private Vector2 center = Vector2.zero;

    public Color TextColor {
        get => new Color(_color.r, _color.g, _color.b, opacity);
    }

    public static FxConfig FxConfig {
        get => ShapesManager.Instance.fxConfig;
    }

    public static TextEffect Create(int i) {
        var prefab = ShapesManager.Instance.textEffectPrefab;
        var textEffect = Instantiate(prefab);
        textEffect.i = i;
        textEffect.fontSizeMultiplier = Random.Range(0.5f, 1.5f);
        int index = Random.Range(0, FxConfig.colors.Count);
        textEffect._color = FxConfig.colors[index];
        return textEffect;
    }

    public override void Draw() {
        var fontSize = FxConfig.fontSize * fontSizeMultiplier;
        Debug.Log("INSIDE DRAW");
        Shapes.Draw.Text(center, content: FxConfig.text, fontSize: fontSize, color: TextColor);
    }

    public void OnAnimationLoopStart() {
        if (Random.Range(0f, 1f) < (1f / 8f)) {
            gameObject.SetActive(false);
        }
        
        int index = Random.Range(0, FxConfig.colors.Count);
        _color = FxConfig.colors[index];
        var xVal = Random.Range(-FxConfig.xRange, FxConfig.xRange);
        var yVal = Random.Range(-FxConfig.yRange, FxConfig.yRange);
        center = new Vector2(1f, 1f) * new Vector2(xVal, yVal);
        fontSizeMultiplier = Random.Range(0.5f, 1.5f);
        var rand = RandomFromDistribution.RandomNormalDistribution(2f, 1f);
        animator.speed = FxConfig.fadeSpeed + Mathf.Clamp(rand, 0f, 4f);
    }

    private void Awake() {
        var original = GetComponent<Animator>();
        animator = Instantiate(GetComponent<Animator>(), transform);
        animator.enabled = true;
    }

    private void OnEnable() {
        var xVal = Random.Range(-FxConfig.xRange, FxConfig.xRange);
        var yVal = Random.Range(-FxConfig.yRange, FxConfig.yRange);
        center = new Vector2(1f, 1f) * new Vector2(xVal, yVal);
    }
}
