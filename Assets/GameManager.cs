using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class GameManager : Singleton<GameManager>
{
    public enum TurnState { Player, Bear }

    [SerializeField]
    public TurnState turnState;

    public delegate void EventHandler(TurnState newTurnState);
    public event EventHandler OnTurnStateChanged;

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

}
