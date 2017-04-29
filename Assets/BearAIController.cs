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
    private float shotAngle = 45f;

    [SerializeField]
    private float throwInterval = 3f;

    private float lastShotTime;

    #region MonoBehaviour

    void Update()
    {
        if (Time.realtimeSinceStartup - lastShotTime > throwInterval)
        {
            lastShotTime = Time.realtimeSinceStartup;

            GameObject ball = Instantiate(spawn, throwOrigin.position, Quaternion.identity);
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            ballRigidbody.velocity = GetBallisticVelocity(target, shotAngle);
            Destroy(ball, 5f);
        }
    }
    #endregion

    #region Private Methods
    private Vector3 GetBallisticVelocity(Transform target, float angle)
    {
        Vector3 dir = target.position - throwOrigin.position;
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
        Vector3 offset = Vector3.forward * 1.6f;


        return target.position + offset;
    }

    #endregion
}