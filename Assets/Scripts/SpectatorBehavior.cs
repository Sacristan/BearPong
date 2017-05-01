using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorBehavior : MonoBehaviour
{

    [SerializeField]
    private Vector3 localOffset = Vector3.up;

    [SerializeField]
    private float durationInSeconds = 3f;

    [SerializeField]
    private float allowMovementDelayTime = 0f;

    private Vector3 _originalPos;

    float perc = 0f;

    private bool allowMovement = false;

    IEnumerator Start()
    {
        _originalPos = transform.localPosition;
        yield return new WaitForSeconds(allowMovementDelayTime);
        allowMovement = true;
    }

    void Update()
    {
        if (!allowMovement) return;
        perc = Mathf.PingPong(Time.time / durationInSeconds, 1f);
        transform.localPosition = Vector3.Lerp(_originalPos, _originalPos + localOffset, perc);
    }
}
