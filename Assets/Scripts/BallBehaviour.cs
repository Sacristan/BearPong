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
		if (other.tag == GameTags.PlayerCup) {
			other.GetComponentInParent<AudioSource> ().Play ();			
		}

        if (!_CupHit)
        {
            _CupHit = true;

            if (other.tag == GameTags.BearCup)
            {
                DrunkEffectController.Instance.GetDrunk();
                //Debug.Log("Cup Hit, bear gets another throw!");
            }
            else if (other.tag == GameTags.PlayerCup)
            {
                Debug.Log("Cup Hit, player gets another throw!");
            }
        }
    }

	public void CallSFX(){
		Debug.Log ("CallSFX");
		GetComponent<AudioSource> ().Play();
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
