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
        if (other.tag == GameTags.PlayerCup)
        {
            other.GetComponentInParent<AudioSource>().Play();
        }

		if (other.tag == GameTags.BearCup)
		{
			GameManager.Instance.BearScored ();
			DrunkEffectController.Instance.GetDrunk();
			BucketFX (other.gameObject);
			//Debug.Log("Cup Hit, bear gets another throw!");
		}
		else if (other.tag == GameTags.PlayerCup)
		{
			GameManager.Instance.PlayerScored ();
			BucketFX (other.gameObject);
			//Debug.Log("Cup Hit, player gets another throw!");
		}
        //Debug.Log (other.tag);

        if (other.tag == GameTags.BallCatchNet)
        {
			if (isCatcheable) {
				isCatcheable = false;
				GameManager.Instance.ShotMissed ();
			}
        }
    }

	private void BucketFX(GameObject g)
	{
		Destroy (g);
	}

    public void MarkCatcheable()
    {
        Debug.Log("MarkCatcheable");
        isCatcheable = true;
    }

    public void CallSFX()
    {
        //Debug.Log ("CallSFX");
        AudioSource audioSource = GetComponent<AudioSource>();
        if(audioSource!=null) audioSource.Play();
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
