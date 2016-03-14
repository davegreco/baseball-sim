using UnityEngine;
using System.Collections;

public class PlayerModel {
	public enum GenderEnum {
		Female,
		Male
	};

	public GenderEnum Gender { get; set; } = GenderEnum.Female;

	public int Age { get; set; } = 0;
	public int HomeRuns { get; set; } = 0;
	public int TotalBallDistance { get; set; } = 0;

	public string Email { get; set; } = "";
	public string FirstName { get; set; } = "";
	public string LastName { get; set; } = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
