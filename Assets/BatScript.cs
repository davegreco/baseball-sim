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
            debugger.Log("Normal: " + normal.ToString(), null, LogType.Log);
            // this is the collision angle
            // you might want to use .velocity instead of .forward here, but it 
            // looks like it's already changed due to bounce in OnCollisionEnter
            var angle = Vector3.Angle(-transform.forward, normal);
            debugger.Log( "Angle: " + angle.ToString(), null, LogType.Log);

            if ( angle > 10 && angle < 90)
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
                rb.velocity = new Vector3((4 * Random.value)  + (normal.x * 5) + ((angle - 60)/20.0f), 22 + (5 * Random.value) - (normal.y *10), 14 + (5 * Random.value) - (normal.z *9));
                debugger.Log("Velocity: " + rb.velocity.ToString(), null, LogType.Log);
            }
            debugger.Log("-----------------------", null, LogType.Log);
        }
    }
}
