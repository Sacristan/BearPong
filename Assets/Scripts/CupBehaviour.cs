using UnityEngine;

public class CupBehaviour : MonoBehaviour
{
    public event CupHandler OnBallEnter;

    public delegate void CupHandler(Transform cup, Transform ball);

    private void OnTriggerEnter(Collider other)
    {
        ///*if (other.tag == "Pickable")*/ OnBallEnter(transform, other.transform);
    }

    private void Start()
    {
        /*OnBallEnter += (cup, ball) => {
            Debug.Log("HIT!!!");
            Debug.Log(ball.tag);
        };*/
    }
}
