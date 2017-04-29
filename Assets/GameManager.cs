using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class GameManager : Singleton<GameManager>
{
    public enum TurnState { Player, Bear }

    [SerializeField]
    public TurnState turnState;

    [SerializeField]
    private BearPongBucket[] bearPongBucketsPlayer;

    [SerializeField]
    private BearPongBucket[] bearPongBucketsBear;

    public BearPongBucket[] BearPongBucketsPlayer { get { return bearPongBucketsPlayer; } }
    public BearPongBucket[] BearPongBucketsBear { get { return bearPongBucketsBear; } }

    public delegate void EventHandler(TurnState newTurnState);
    public event EventHandler OnTurnStateChanged;

    private void Start()
    {
        BallSpawner.Instance.SpawnBall();
    }

    public void ToggleTurn()
    {
        switch (turnState)
        {
            case TurnState.Player:
                turnState = TurnState.Bear;
                break;
            case TurnState.Bear:
                turnState = TurnState.Player;
                break;
            default:
                throw new System.NotImplementedException();
        }

        if (OnTurnStateChanged != null) OnTurnStateChanged.Invoke(turnState);
    }

    public void PlayerScored()
    {

    }

    public void BearScored()
    {
        DrunkEffectController.Instance.GetDrunk();
    }

    public void ShotMissed()
    {
        Debug.Log("ShotMissed");
        ToggleTurn();
    }

}
