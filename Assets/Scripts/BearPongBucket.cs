using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPongBucket : MonoBehaviour
{
    [SerializeField]
    private Transform throwTarget;

    public Transform ThrowTarget { get { return throwTarget; } }
}
