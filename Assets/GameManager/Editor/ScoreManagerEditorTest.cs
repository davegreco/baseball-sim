using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ScoreManagerEditorTest {

    [Test]
    public void ScoreSaveTest()
    {
        // Arrange
		var player = new PlayerModel();
		player.Age = 21;
		player.Email = "test@test.com";
		player.Gender = PlayerModel.GenderEnum.Male;
		player.FirstName = "Jon";
		player.HomeRuns = 3;
		player.LastName = "Smith";
		player.TotalBallDistance = 1250;

		var score = new ScoreModel();
		score.Player = player;
		score.Score = 1500;

		var scoreManager = new ScoreManager();

        // Act
        // Try to submit score.
		scoreManager.SubmitScore(score);

        // Assert
        // The object saved and loaded.
		Assert.IsNotEmpty(scoreManager.Scores);

		// Act
		// Create new ScoreManager.
		var savedScoreManager = new ScoreManager();

		// Assert
		// Scores is empty.
		Assert.IsEmpty(savedScoreManager.Scores);

		// Act
		// Load scores.
		savedScoreManager.LoadScores();

		// Assert
		// Scores is not empty.
		Assert.IsNotEmpty(savedScoreManager.Scores);

		// Act
		// Get loaded score.
		var loadedScore = savedScoreManager.Scores[0];

		// Assert
		// The loaded score retained original values.
		Assert.AreEqual(loadedScore.Score, score.Score);
		Assert.AreEqual(loadedScore.Player.Age, score.Player.Age);
		Assert.AreEqual(loadedScore.Player.Email, score.Player.Email);
		Assert.AreEqual(loadedScore.Player.FirstName, score.Player.FirstName);
		Assert.AreEqual (loadedScore.Player.Gender, score.Player.Gender);
		Assert.AreEqual(loadedScore.Player.HomeRuns, score.Player.HomeRuns);
		Assert.AreEqual(loadedScore.Player.LastName, score.Player.LastName);
		Assert.AreEqual(loadedScore.Player.TotalBallDistance, score.Player.TotalBallDistance);
    }
}
