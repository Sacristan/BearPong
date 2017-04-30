using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class DrunkEffectController : Singleton<DrunkEffectController>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float drunkinessLevel = 0f;

    private readonly MinMax cameraFOVMinMax = new MinMax(60, 90);
    private readonly MinMax blurMinMax = new MinMax(0, 7);

    private Camera _camera;
    private VignetteAndChromaticAberration _vignetteAndChromaticAberration;
    private BlurOptimized _Blur;

    public void GetDrunk()
    {
        drunkinessLevel = Mathf.Clamp(drunkinessLevel + 0.3f, 0f, 0.9f);
    }

    #region MonoBehaviour

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _vignetteAndChromaticAberration = GetComponent<VignetteAndChromaticAberration>();
        _Blur = GetComponent<BlurOptimized>();
    }

    private void Update()
    {
        float targetFOV = Mathf.Lerp(cameraFOVMinMax.min, cameraFOVMinMax.max, drunkinessLevel);
        _camera.fieldOfView = targetFOV;
        _Blur.blurSize = Mathf.Lerp(blurMinMax.min, blurMinMax.max, drunkinessLevel);
        _vignetteAndChromaticAberration.intensity = drunkinessLevel;
        _vignetteAndChromaticAberration.blur = drunkinessLevel;
    }

    #endregion
}
