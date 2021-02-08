using UniRx;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ShapesManager : MonoBehaviour {
    public int numberOfCircles = 10;

    public Camera camera;
    public FxConfig fxConfig;
    public TextEffect textEffectPrefab;
    public AnimationCurve maxRadiusCurve;
    public CircleEffect circleEffectPrefab;
    public AnimationCurve thicknessDeltaCurve;
    public List<EffectBehaviour> effects = new List<EffectBehaviour>();

    private static ShapesManager m_instance;
    private static Color[] colors = new Color[3] {Color.black, Color.gray, Color.white};

    private int colorIdx = -1;
    private bool endingIntro = false;

    public static ShapesManager Instance {
        get {
            if (m_instance == null) return m_instance;
            
            return m_instance = FindObjectOfType<ShapesManager>();
        }
    }

    public void Init() {
        Deinit();
        effects = new List<EffectBehaviour>();
        CreateEffects();
    }

    public void Deinit() {
        effects.ForEach(effect => {
            effect.Dispose();
            Destroy(effect);
        });
        effects = new List<EffectBehaviour>();
    }

    public void OnEndCameraRendering() {
        if (endingIntro) return;
        
        var activeEffects = from e in effects where e.isActiveAndEnabled select e;
        foreach (var effect in activeEffects) {
            effect.Draw();
        }
        
        if (activeEffects.ToList().Count == 0) {
            endingIntro = true;
            // InvokeRepeating(nameof(LoopBackgroundColorChange), 0f, 0.5f);
            PostProcessingManager.Instance.TweenToGrayscale();
        }
    }

    private void LoopBackgroundColorChange() {
        colorIdx = (colorIdx + 1) % 3;
        camera.backgroundColor = colors[colorIdx];
    }

    private void Awake() {
        if (m_instance == null) {
            m_instance = this;
        }
        else {
            Destroy(m_instance);
        }
    }

    private void CreateEffects() {
        foreach (var i in Enumerable.Range(0, numberOfCircles)) {
            effects.Add(CircleEffect.Create(Random.value * i / 5f));
        }
        
        effects.Add(TextEffect.Create(0));
        effects.Add(TextEffect.Create(1));
        effects.Add(TextEffect.Create(2));
        effects.Add(TextEffect.Create(3));
    }
}