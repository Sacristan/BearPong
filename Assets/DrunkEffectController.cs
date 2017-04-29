using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class DrunkEffectController : MonoBehaviour
{
    private struct MinMax
    {
        public float min;
        public float max;

        public MinMax(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float drunkinessLevel = 0f;

    private readonly MinMax cameraFOVMinMax = new MinMax(60, 90);

    private Camera _camera;
    private VignetteAndChromaticAberration _vignetteAndChromaticAberration;

    #region MonoBehaviour

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _vignetteAndChromaticAberration = GetComponent<VignetteAndChromaticAberration>();
    }

    private void Update()
    {
        float targetFOV = Mathf.Lerp(cameraFOVMinMax.min, cameraFOVMinMax.max, drunkinessLevel);
        _camera.fieldOfView = targetFOV;

        _vignetteAndChromaticAberration.intensity = drunkinessLevel;
        _vignetteAndChromaticAberration.blur = drunkinessLevel;
    }

    #endregion
}
