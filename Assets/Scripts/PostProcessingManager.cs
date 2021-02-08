using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour {
    public Volume Volume;

    [SerializeField] private AnimationCurve _animationCurve;
    private ColorAdjustments _colorAdjustments;
    private static PostProcessingManager m_instance;

    private float _saturation {
        get => _colorAdjustments.saturation.value;
        set => _colorAdjustments.saturation.value = value;
    }

    public static PostProcessingManager Instance {
        get {
            if (m_instance == null) return m_instance;

            return m_instance = FindObjectOfType<PostProcessingManager>();
        }
    }

    public void TweenToGrayscale() {
        var tween = DOTween.To(() => _saturation, x => _saturation = x, -100f, 5);
        tween.SetEase(_animationCurve);
    }

    private void Awake() {
        if (m_instance == null) {
            m_instance = this;
        } else {
            Destroy(m_instance);
            return;
        }

        Volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
        Debug.Log("_colorAdjustments", _colorAdjustments);
    }
}