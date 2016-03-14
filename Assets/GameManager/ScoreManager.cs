using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class ScoreManager {
	public int HighScoreThreshold { get; set; } = 100;

	public List<ScoreModel> Scores { get; set; } = new List<ScoreModel>();

	public List<ScoreModel> ListHighScores(int limit = 10) {
		// TODO: Implement a return of the high scores
		return null;
	}

	// Use this determine if a score is a high score or not.
	// Returns true if a score is a high score and false if it is not.
	public bool IsHighScore(ScoreModel score) {
		return Scores.IndexOf(score) <= HighScoreThreshold ? true : false;
	}

	// Use this to get the current count of recorded scores.
	public int ScoreCount() {
		// TODO: Implement a score count.
		return Scores.Count;
	}

	// Use this to submit a new score.
	// Returns the rank of the new score.
	public int SubmitScore(ScoreModel score) {
		// TODO: Implement score saving.
		Scores.Add(score);
		Scores.OrderByDescending(x => x.Score);

		SaveScores();

		return Scores.IndexOf(score);
	}

	public void LoadScores() {
		var serializer = new XmlSerializer(Scores.GetType(), "ScoresManger.Scores");

		object obj;

		using (var reader = new StreamReader("scores.xml")) {
			obj = serializer.Deserialize(reader.BaseStream);
		}

		Scores = (List<ScoreModel>)obj;
	}

	public void ResetScores() {
		Scores = new List<ScoreModel> ();

		SaveScores();
	}

	public void SaveScores() {
		var serializer = new XmlSerializer(Scores.GetType(), "ScoresManger.Scores");

		using (var writer = new StreamWriter("scores.xml", false)) {
			serializer.Serialize(writer.BaseStream, Scores);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
