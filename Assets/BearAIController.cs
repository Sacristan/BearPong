#define DEBUG_BEAR_BEHAVIOUR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAIController : MonoBehaviour
{
    [SerializeField]
    private Transform throwOrigin;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private GameObject spawn;

    [SerializeField]
    private MinMax shotAngle = new MinMax(35, 45);

    [SerializeField]
    private float throwInterval = 3f;

    private float lastShotTime;

    #region MonoBehaviour

    private void Start()
    {
        GameManager.Instance.OnTurnStateChanged += Instance_OnTurnStateChanged;
    }

#if DEBUG_BEAR_BEHAVIOUR
    private void Update()
    {
        if (Time.realtimeSinceStartup - lastShotTime > throwInterval) Shoot();
    }
#endif
    #endregion

    #region Private Methods
    private void Shoot()
    {
        lastShotTime = Time.realtimeSinceStartup;

        GameObject ball = Instantiate(spawn, throwOrigin.position, Quaternion.identity);
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.velocity = GetBallisticVelocity(target, shotAngle.RandomFromRange());
        Destroy(ball, 5f);
    }

    private Vector3 GetBallisticVelocity(Transform target, float angle)
    {
        Vector3 dir = GetTargetPosition() - throwOrigin.position;
        float h = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);

        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    private Vector3 GetTargetPosition()
    {
        return target.position;
    }

    #endregion

    #region Event Callbacks
    private void Instance_OnTurnStateChanged(GameManager.TurnState newTurnState)
    {
        switch (newTurnState)
        {
            case GameManager.TurnState.Bear:
                break;
            case GameManager.TurnState.Player:
                break;
        }
    }
    #endregion
}