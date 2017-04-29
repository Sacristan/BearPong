using System.Collections;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Range(2, 20)]
    [SerializeField]
    private float _Lifetime = 5f;
    private bool _CupHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_CupHit && (other.tag == GameTags.Cup))
        {
            _CupHit = true;
            Debug.Log("Cup Hit, another throw!");
            Destroy(gameObject);
        }
    }

    public IEnumerator KillInSeconds()
    {
        yield return new WaitForSeconds(_Lifetime);
        if (!_CupHit)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }
    
    void Update()
    {

    }
}
