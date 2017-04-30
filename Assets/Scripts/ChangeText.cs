using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {
	private TextMesh _text;

	void Start(){
		_text = GetComponent<TextMesh> ();
		GameManager.Instance.OnFirstTurnStarted += ChangeTextBasedOnTurn;
		GameManager.Instance.OnTurnStateChanged += ChangeTextBasedOnTurn;
	}

	private void ChangeTextBasedOnTurn(GameManager.TurnState newState){
		switch(newState){
		case GameManager.TurnState.Player:
			_text.text = "Its now Your turn!";
			break;
		case GameManager.TurnState.Bear:
			_text.text = "Its now Bear's turn!";
			break;
		}
	}

}
