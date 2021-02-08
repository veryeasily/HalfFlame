using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour {
    public ShapesManager shapesManager;
    private static GameManager m_instance;

    public static GameManager Instance {
        get {
            if (m_instance == null) {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    // public static float Gaussian() {
    //     Vector2 v = Random.insideUnitCircle;
    //     float s = v.sqrMagnitude;
    //     return v.x * Mathf.Sqrt((2f * Mathf.Log(s)) / s);
    // }

    // public static float Gaussian(float mean, float dev) {
    //     return mean + Gaussian() * dev;
    // }

    private void Awake() {
        Log("Awake");
        if (m_instance == null) {
            m_instance = this as GameManager;
        }
    }

    private void Log(string message) {
        Debug.Log("GameManager: " + message);
    }

    // private void OnEnable() {
    //     Log("OnEnable");
    // }

    private void Start() {
        Log("Start");
        Log("enabled: " + enabled);
        Log("isActiveAndEnabled: " + isActiveAndEnabled);
        Log("Application.isPlaying: " + Application.isPlaying);
        if (m_instance == null) {
            m_instance = this as GameManager;
        }
        shapesManager.Init();
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDestroy() {
        Log("OnDestroy");
        shapesManager.Deinit();
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera) {

        shapesManager.OnEndCameraRendering();
    }

    public void OnExit() {
        Application.Quit();
    }
}
