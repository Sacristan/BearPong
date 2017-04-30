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

    //[SerializeField]
    private MinMax throwWaitTime = new MinMax(3f, 6f);

    private float throwInterval = 3f;

    private float lastShotTime;

    private int drunkLevel = 0;

    private List<BearPongBucket> bearPongBuckets;

    private Transform Target
    {
        get
        {
            bearPongBuckets.RemoveAll(x => x == null);
            int idx = UnityEngine.Random.Range(0, bearPongBuckets.Count);
            BearPongBucket bearPongBucket = bearPongBuckets[idx];
            return bearPongBucket.ThrowTarget;
        }
    }

    private float ThrowErrorArea
    {
        get
        {
			if (drunkLevel == 0) return 0.1f;
            if (drunkLevel == 1) return 0.25f;
            if (drunkLevel == 2) return 0.35f;
            if (drunkLevel == 3) return 0.5f;

            return 0;
        }
    }

    #region MonoBehaviour

    private void Start()
    {
        GameManager.Instance.OnTurnStateChanged += Instance_OnTurnStateChanged;
        this.bearPongBuckets = new List<BearPongBucket>(GameManager.Instance.BearPongBucketsBear);
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
		GetComponent<AudioSource> ().Play ();
		ball.tag = GameTags.Untagged;
        ballRigidbody.velocity = GetBallisticVelocity(Target, shotAngle.RandomFromRange());
        Destroy(ball, 5f);
    }

    internal void TriggerDrunk()
    {
        drunkLevel++;
		Debug.LogFormat("Bear drunk level now is: {0} and throw error area: {1}",drunkLevel, ThrowErrorArea);
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
			UnityEngine.Random.Range(-ThrowErrorArea, ThrowErrorArea),
			UnityEngine.Random.Range(-ThrowErrorArea, ThrowErrorArea),
			UnityEngine.Random.Range(-ThrowErrorArea, ThrowErrorArea)
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