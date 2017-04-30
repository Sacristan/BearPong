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

    private int playerScore = 0;
    private int bearScore = 0;

    private const int MaxScore = 3;

    private void Start()
    {
        switch (turnState)
        {
            case TurnState.Bear:
                BearAIController.Instance.QueueThrow();
                break;
            case TurnState.Player:
                BallSpawner.Instance.SpawnBall();
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public void ToggleTurn()
    {
        TakeAction();
        if (OnTurnStateChanged != null) OnTurnStateChanged.Invoke(turnState);
    }

    private void TakeAction()
    {
        switch (turnState)
        {
            case TurnState.Player:
                turnState = TurnState.Bear;
                BearAIController.Instance.QueueThrow();
                break;
            case TurnState.Bear:
                turnState = TurnState.Player;
                BallSpawner.Instance.SpawnBall();
                break;
            default:
                throw new System.NotImplementedException();
        }
    }

    public void PlayerScored()
    {
        BearAIController.Instance.TriggerDrunk();
        if (++playerScore >= MaxScore) TriggerWin();
        BallSpawner.Instance.SpawnBall();
    }

    public void BearScored()
    {
        DrunkEffectController.Instance.GetDrunk();
        if (++bearScore >= MaxScore) TriggerWin();
        BearAIController.Instance.QueueThrow();
    }

    public void ShotMissed()
    {
        Debug.Log("ShotMissed");
        ToggleTurn();
    }

    private void TriggerWin()
    {
        throw new System.NotImplementedException();
    }

    private void TriggerLose()
    {
        throw new System.NotImplementedException();
    }
}
