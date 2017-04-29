﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class DrunkEffectController : Singleton<DrunkEffectController>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float drunkinessLevel = 0f;

    private readonly MinMax cameraFOVMinMax = new MinMax(60, 90);

    private Camera _camera;
    private VignetteAndChromaticAberration _vignetteAndChromaticAberration;

    public void GetDrunk()
    {
        drunkinessLevel += 0.3f;
    }

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
