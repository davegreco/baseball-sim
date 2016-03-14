using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	protected GameManager() {}

	public ScoreManager ScoreManager { get; set; } = new ScoreManager();

	public Queue<PlayerModel> Players { get; set; } = new Queue<PlayerModel>();

	public void QueuePlayer(PlayerModel player) {
		// TODO: Kick off the next round if not already running.
		Players.Enqueue(player);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Players
}
