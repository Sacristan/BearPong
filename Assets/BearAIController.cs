//#define DEBUG_BEAR_BEHAVIOUR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAIController : Singleton<BearAIController>
{
    [SerializeField]
    private Transform throwOrigin;

    [SerializeField]
    private GameObject spawn;

    [SerializeField]
    private MinMax shotAngle = new MinMax(35, 45);

    [SerializeField]
    private MinMax throwWaitTime = new MinMax(1f, 3f);

    [SerializeField]
    private float throwInterval = 3f;

    private float lastShotTime;

    private int drunkLevel = 0;

    private Transform Target
    {
        get
        {
            BearPongBucket[] bearPongBuckets = GameManager.Instance.BearPongBucketsBear;
            int idx = UnityEngine.Random.Range(0, bearPongBuckets.Length);
            BearPongBucket bearPongBucket = bearPongBuckets[idx];
            return bearPongBucket.ThrowTarget;
        }
    }

    private float Area
    {
        get
        {
            if (drunkLevel == 1) return 0.1f;
            if (drunkLevel == 2) return 0.2f;
            if (drunkLevel == 3) return 0.5f;

            return 0;
        }
    }

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

    #region Public Methods

    public void QueueThrow()
    {
        StartCoroutine(ThrowRoutine());
    }
#endregion

    #region Private Methods
    private IEnumerator ThrowRoutine()
    {
        yield return new WaitForSeconds(throwWaitTime.RandomFromRange());
        Throw();
    }

    private void Throw()
    {
        lastShotTime = Time.realtimeSinceStartup;

        GameObject ball = Instantiate(spawn, throwOrigin.position, Quaternion.identity);
        ball.GetComponent<BallBehaviour>().MarkCatcheable();
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.velocity = GetBallisticVelocity(Target, shotAngle.RandomFromRange());
        Destroy(ball, 5f);
    }

    internal void TriggerDrunk()
    {
        drunkLevel++;
        Debug.LogFormat("Bear drunk level now is: {0} and miscalculation area: {1}",drunkLevel, Area);
    }

    private Vector3 GetBallisticVelocity(Transform target, float angle)
    {
        Vector3 dir = GetTargetPosition(target) - throwOrigin.position;
        float h = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);

        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    private Vector3 GetTargetPosition(Transform t)
    {
        Vector3 drunkCorrection = new Vector3(
           UnityEngine.Random.Range(-Area, Area),
           UnityEngine.Random.Range(-Area, Area),
           UnityEngine.Random.Range(-Area, Area)
        );

        return t.position + drunkCorrection;
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