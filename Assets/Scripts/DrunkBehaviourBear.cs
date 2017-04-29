using System.Collections;
using UnityEngine;
//using UnityStandardAssets.ImageEffects;

//[RequireComponent(typeof(Camera))]
//[RequireComponent(typeof(VignetteAndChromaticAberration))]
public class DrunkBehaviourBear : MonoBehaviour
{
    //TODO add some messsing with aim or something based on Drunkenness, same for the player

    //private Camera _Camera;
    //private VignetteAndChromaticAberration _Effect;
    private float _Drunkenness = 0f;
    private float _MaxDrunkenness = 1f;
    private float _MinDrunkenness = 0f;
    private float _TargetDrunkenness = 0f;
    [SerializeField]
    private float _DrunkennessTimeInSeconds = 1f;
    private float _TimeSinceLastDistortion = 0f;
    [SerializeField]
    private float _DistortionIntervalInSeconds = 1f;
    [SerializeField]
    private float _DistortionRandomFactor = 0.1f;
    //[SerializeField]
    //private int _CameraMin = 60;
    //[SerializeField]
    //private int _CameraMax = 90;
    private bool _InDrunk = false;

    public float Drunkenness
    {
        get
        {
            return _Drunkenness;
        }

        set
        {
            _TargetDrunkenness = Mathf.Clamp(value, _MinDrunkenness, _MaxDrunkenness);
            StartCoroutine(GetDrunk(_DrunkennessTimeInSeconds));
        }
    }

    private void Start()
    {
        //_Camera = GetComponent<Camera>();
        //_Effect = GetComponent<VignetteAndChromaticAberration>();
    }

    private void Update()
    {
        if (!_InDrunk)
        {
            _TimeSinceLastDistortion += Time.deltaTime;
            if (_TimeSinceLastDistortion >= _DistortionIntervalInSeconds)
            {
                StartCoroutine(
                    GetDrunk(Mathf.Clamp(
                        Random.Range(
                            _Drunkenness - _DistortionRandomFactor,
                            _Drunkenness + _DistortionRandomFactor),
                        _MinDrunkenness,
                        _MaxDrunkenness),
                    _DistortionIntervalInSeconds));
                _TimeSinceLastDistortion = 0f;
            }
        }
    }

    private void EditEffectAndCamera(float drunkenness)
    {
        //_Effect.intensity = drunkenness;
        //_Camera.fieldOfView = _CameraMin + ((_CameraMax - _CameraMin) * drunkenness);
        //do some visual/audio stuff here, stumble around, roar or whatever drunken bears do ...
    }

    private IEnumerator GetDrunk(float target, float timeInSeconds)
    {
        if (!_InDrunk)
        {
            _InDrunk = true;
            float localDrunkenness = _Drunkenness;
            float deltaDrunkenness = target - localDrunkenness;
            float rate = deltaDrunkenness / timeInSeconds;
            if (rate > 0)
            {
                while (localDrunkenness < target)
                {
                    localDrunkenness += Time.deltaTime * rate;
                    EditEffectAndCamera(localDrunkenness);
                    yield return new WaitForEndOfFrame();
                }
                while (localDrunkenness >= _Drunkenness)
                {
                    localDrunkenness -= Time.deltaTime * rate;
                    EditEffectAndCamera(localDrunkenness);
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                while (target < localDrunkenness)
                {
                    localDrunkenness += Time.deltaTime * rate;
                    EditEffectAndCamera(localDrunkenness);
                    yield return new WaitForEndOfFrame();
                }
                while (_Drunkenness >= localDrunkenness)
                {
                    localDrunkenness -= Time.deltaTime * rate;
                    EditEffectAndCamera(localDrunkenness);
                    yield return new WaitForEndOfFrame();
                }
            }
            _InDrunk = false;
            //just in case Drunkenness got set while we were messing around
            StartCoroutine(GetDrunk(_DrunkennessTimeInSeconds));
        }
    }

    private IEnumerator GetDrunk(float timeInSeconds)
    {
        if (!_InDrunk)
        {
            _InDrunk = true;
            float deltaDrunkenness = _TargetDrunkenness - _Drunkenness;
            float rate = deltaDrunkenness / timeInSeconds;
            if (rate > 0)
            {
                while (_Drunkenness < _TargetDrunkenness)
                {
                    _Drunkenness += Time.deltaTime * rate;
                    EditEffectAndCamera(_Drunkenness);
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                while (_TargetDrunkenness < _Drunkenness)
                {
                    _Drunkenness += Time.deltaTime * rate;
                    EditEffectAndCamera(_Drunkenness);
                    yield return new WaitForEndOfFrame();
                }
            }
            _InDrunk = false;
        }
    }
}
