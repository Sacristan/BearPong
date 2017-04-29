using System.Collections;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    [Range(2, 20)]
    [SerializeField]
    private float _Lifetime = 5f;
    private bool _CupHit = false;

    private bool isCatcheable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_CupHit)
        {
            _CupHit = true;
            if (other.tag == GameTags.BearCup)
            {
                GameManager.Instance.BearScored();
                Debug.Log("Cup Hit, bear gets another throw!");
            }
            else if (other.tag == GameTags.PlayerCup)
            {
                GameManager.Instance.PlayerScored();
                Debug.Log("Cup Hit, player gets another throw!");
            }
            else if (isCatcheable && other.tag == GameTags.BallCatchNet)
            {
                GameManager.Instance.ShotMissed();
            }
        }
    }

    public void MarkCatcheable()
    {
        isCatcheable = true;
    }

    public void CallSFX()
    {
        GetComponent<AudioSource>().Play();
    }

    public IEnumerator KillInSeconds()
    {
        yield return new WaitForSeconds(_Lifetime);
        if (!_CupHit)
        {
            Destroy(gameObject);
        }
    }
}
