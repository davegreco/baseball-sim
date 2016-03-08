using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PittchScript : MonoBehaviour {

    bool pitch_now = false;
    Vector3 pitchersHand;
    Vector3 endPoint;
    Vector3 startPoint;
    float duration = 0.5f;
    float startTime;

    public float pitchEverySeconds = 5.5f;
    private float nextActionTime = 0.0f;

    private TrailRenderer _trails;

    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        nextActionTime = Time.time + pitchEverySeconds;
        _trails = GetComponent<TrailRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (pitch_now)
        {
            audio.Play();
            transform.position = new Vector3(-0.4f, 1.44f, 15.19f);
            _trails.time = 0.25f;
            _trails.startWidth = 0.2f;
            _trails.endWidth = 0.1f;

            // Fastball
            //GetComponent<Rigidbody>().velocity = new Vector3(0.4f, -0.05f, -33.19f);

            // Offspeed
            // GetComponent<Rigidbody>().velocity = new Vector3(0.4f, 2.4f, -20.19f);


            // Changeup
            // GetComponent<Rigidbody>().velocity = new Vector3(0.4f, 4.4f, -15.19f);  // High
            GetComponent<Rigidbody>().velocity = new Vector3(0.4f, 3.8f, -15.19f);  // High

            nextActionTime = Time.time + pitchEverySeconds;
            pitch_now = false;
        }
        else if (Input.GetKeyDown("p") || Time.time > nextActionTime)
        {
            //if (pitchersHand == null)
            //{
            //    pitchersHand = transform.position;
            //}
            _trails.time = 0.0f;
            pitch_now = true;

            //startTime = Time.time;
            //startPoint = transform.position;
            //is_pitching = true;
            //endPoint = new Vector3(0.0f, 0.2f, -4);
        }

    }
}
