using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleEffect : EffectBehaviour {
    public bool Visible;
    public Vector2 Center;
    public float Duration;
    public Color Color;
    public float Radius = 0f;

    private CircleEffect _circleEffect;

    [SerializeField] private float _thicknessDelta;

    private Tweener _tweener;

    [SerializeField] private float endRadius = 1f;

    private Coroutine interval;
    private Coroutine intervalTwo;

    public static FxConfig FxConfig => ShapesManager.Instance.fxConfig;

    public static CircleEffect Create(float endRadius) {
        var prefab = ShapesManager.Instance.circleEffectPrefab;

        var circleEffect = Instantiate(prefab);
        circleEffect.Radius = 1f;
        circleEffect.Duration = 1f;
        circleEffect.Visible = true;
        circleEffect._thicknessDelta = 0f;
        circleEffect.Center = Vector2.zero;
        circleEffect.Color = Color.black;

        circleEffect._circleEffect = circleEffect;
        circleEffect.endRadius = endRadius;
        return circleEffect;
    }

    public override void Draw() {
        if (!Visible) {
            return;
        }

        var thickness = GetThickness();

        Shapes.Draw.Ring(Center, Radius, thickness: thickness * 1.06125f, color: Color.black);
        Shapes.Draw.Ring(Center, Radius, thickness: thickness, color: Color);
        Shapes.Draw.Ring(Center, Radius, thickness: thickness / 20f, color: Color.blue);
    }

    public void OnDisable() {
        if (interval != null) {
            StopCoroutine(interval);
        }

        if (intervalTwo != null) {
            StopCoroutine(intervalTwo);
        }
    }

    public void Reset() {
        _circleEffect.MaybeDeactivate();
        _thicknessDelta = 0f;
        Color = GetRandomColor();
        Duration = GetRandomDuration();
        Center = Random.insideUnitCircle * FxConfig.centerMultiplier;
        Radius = 0;

        // var rand = ShapesManager.Instance.maxRadiusCurve.Evaluate(Random.value);
        // FxConfig.maxRadius = Mathf.Max(rand, 0.1f);
        // circle.Reset();
        // animator.speed = 1f / circle.Duration;

        var maxRadius = _circleEffect.endRadius * FxConfig.maxRadius;
        _tweener = DOTween.To(() => Radius, x => Radius = x, maxRadius, Duration);
        _tweener.onComplete += Reset;
    }

    private static float GetRandomDuration() {
        var sample = RandomFromDistribution.RandomNormalDistribution(FxConfig.meanTime, FxConfig.stdDevTime);
        return Mathf.Max(0.05f, sample);
    }

    private static Color GetRandomColor() {
        var sample = RandomFromDistribution.RandomNormalDistribution(1f, 1f);
        return sample < 0 ? Color.yellow : Color.red;
    }

    private void MaybeDeactivate() {
        if (Random.Range(0f, 1f) < (1f / 1.33333333f)) {
            gameObject.SetActive(false);
        }
    }

    private float GetThickness() {
        var thickness = FxConfig.thickness;
        return thickness + _thicknessDelta;
    }

    private void Tick() {
        var rand = (ShapesManager.Instance.thicknessDeltaCurve.Evaluate(Random.value) - 0.5f) / 4f;
        _thicknessDelta = Mathf.Min(Mathf.Max(rand + _thicknessDelta, -0.2f), 0.2f);
    }

    private void MaybeHide() {
        Visible = !(Random.value < FxConfig.hideProbability);
    }

    private void Start() {
        interval = StartCoroutine(WaitAndRandomlyToggleVisibility());
        intervalTwo = StartCoroutine(WaitAndTickCircle());
        Reset();
    }

    private IEnumerator WaitAndTickCircle() {
        yield return new WaitForSeconds(1f / 60f);
        Tick();
        if (gameObject.activeInHierarchy) {
            yield return WaitAndTickCircle();
        }
    }

    private IEnumerator WaitAndRandomlyToggleVisibility() {
        var timespan = RandomFromDistribution.RandomNormalDistribution(0.5f, 0.25f);
        yield return new WaitForSeconds(Mathf.Max(0.025f, timespan));
        MaybeHide();
        if (gameObject.activeInHierarchy) {
            yield return WaitAndRandomlyToggleVisibility();
        }
    }
}