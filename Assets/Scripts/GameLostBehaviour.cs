using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GameLostBehaviour : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Blackout());
    }

    private IEnumerator Blackout()
    {
        VignetteAndChromaticAberration eff = GetComponentInParent<VignetteAndChromaticAberration>();
        while (eff.intensity > float.Epsilon)
        {
            eff.intensity -= Time.deltaTime / 4;
            yield return new WaitForEndOfFrame();
        }
    }
}
