using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BatScript : MonoBehaviour {

    GuiDebugScript debugger;
    AudioSource audio;

    // Use this for initialization
    void Start () {
        debugger = gameObject.AddComponent<GuiDebugScript>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

    }

    void OnCollisionEnter(Collision info)
    {
        debugger.Log("Collision", null, LogType.Log);

        // http://answers.unity3d.com/questions/279634/collisions-getting-the-normal-of-the-collision-sur.html
        // find collision point and normal. You may want to average over all contacts
        var point = info.contacts[0].point;
        var dir = -info.contacts[0].normal; // you need vector pointing TOWARDS the collision, not away from it
                                            // step back a bit
        point -= dir;
        RaycastHit hitInfo;
        // cast a ray twice as far as your step back. This seems to work in all
        // situations, at least when speeds are not ridiculously big
        if (info.collider.Raycast(new Ray(point, dir), out hitInfo, 12))
        {
            // this is the collider surface normal
            var normal = hitInfo.normal;
            //debugger.Log("Normal: " + normal.ToString(), null, LogType.Log);
            // this is the collision angle
            // you might want to use .velocity instead of .forward here, but it 
            // looks like it's already changed due to bounce in OnCollisionEnter

            // angle_fly_ball is the up/down angle the ball comes off the bat with 180 degrees being straight down, 90 degrees being a flat line drive, and 0 degrees being straight up
            var angle_fly_ball = Vector3.Angle(transform.right, normal);
            debugger.Log("angle_fly_ball: " + angle_fly_ball.ToString(), null, LogType.Log);

            // angle_first_to_third is 45 degrees down first base line, 90 degrees up the middle, and 135 degrees down the third base line 
            var angle_first_to_third = Vector3.Angle(transform.up, normal);    
            //debugger.Log("angle_first_to_third: " + angle_first_to_third.ToString(), null, LogType.Log);


            if (angle_fly_ball > 10 && angle_fly_ball < 170)
            {
                audio.Play();
                TrailRenderer _trails = info.gameObject.GetComponent<TrailRenderer>();
                if (_trails != null)
                {
                    _trails.time = 2.0f;
                    _trails.startWidth = 0.3f;
                    _trails.endWidth = 0.1f;
                }
                Rigidbody rb = info.rigidbody;
                //Vector3 n = info.contacts[0].normal;
                float dvx = (angle_first_to_third < 90) ? -1.0f : 1.0f;
                float vx = dvx * Mathf.Sqrt(Mathf.Abs(angle_first_to_third - 90) * 5);
                float dvy = (angle_fly_ball < 90) ? -1.0f : 1.0f;
                float vy = dvy * Mathf.Abs(20 * Mathf.Cos(angle_fly_ball));
                float vz = 14 + (5 * Random.value) - (normal.z * 9);

                rb.velocity = new Vector3(vx, vy, vz);
                debugger.Log("Velocity: " + rb.velocity.ToString(), null, LogType.Log);
            }
            debugger.Log("-----------------------", null, LogType.Log);
        }
    }
}
