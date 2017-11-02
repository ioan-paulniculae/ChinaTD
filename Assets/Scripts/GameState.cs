using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState {

	public int persistentCurrency = 0;

	public GameState(int persistentCurrency) {
		this.persistentCurrency = persistentCurrency;
	}
}
