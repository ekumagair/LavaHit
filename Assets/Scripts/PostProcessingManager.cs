using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }
    public PostProcessVolume Volume { get; private set; }
    public float Weight { get; private set; } = 1.0f;

    public Bloom Bloom => _bloom;
    private Bloom _bloom;

    public AmbientOcclusion AmbientOcclusion => _ambientOcclusion;
    private AmbientOcclusion _ambientOcclusion;

    public MotionBlur MotionBlur => _motionBlur;
    private MotionBlur _motionBlur;

    public ColorGrading ColorGrading => _colorGrading;
    private ColorGrading _colorGrading;

    public ChromaticAberration ChromaticAberration => _chromaticAberration;
    private ChromaticAberration _chromaticAberration;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Volume = GetComponent<PostProcessVolume>();
        Volume.profile.TryGetSettings(out _bloom);
        Volume.profile.TryGetSettings(out _ambientOcclusion);
        Volume.profile.TryGetSettings(out _motionBlur);
        Volume.profile.TryGetSettings(out _colorGrading);
        Volume.profile.TryGetSettings(out _chromaticAberration);
    }

    void Start()
    {
        GetSettings();
    }

    public void GetSettings()
    {
        Weight = OptionsManager.Instance.PostProcessWeight;
        Volume.weight = OptionsManager.Instance.PostProcessWeight;
        _bloom.active = OptionsManager.Instance.PostProcessBloom;
        _ambientOcclusion.active = OptionsManager.Instance.PostProcessAmbientOcclusion;
        _motionBlur.active = OptionsManager.Instance.PostProcessMotionBlur;
        _colorGrading.active = OptionsManager.Instance.PostProcessColorGrading;
        _chromaticAberration.active = OptionsManager.Instance.PostProcessChromaticAberration;
    }
}
