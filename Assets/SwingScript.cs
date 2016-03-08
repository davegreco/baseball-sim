using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class SwingScript : MonoBehaviour {
    public Quaternion originalRotationValue; // declare this as a Quaternion
    private Vector3 initialGripPosition;

    Kinect.Joint lastLeftHand;
    Kinect.Joint lastRightHand;

    int steps_left = 0;

    GuiDebugScript debugger;


    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject bottomHand;
    public GameObject topHand;


    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    // Use this for initialization
    void Start () {
        originalRotationValue = transform.rotation; // save the initial rotation
        initialGripPosition = transform.position;
        debugger = gameObject.AddComponent<GuiDebugScript>();
    }

    // Update is called once per frame
    void Update () {
        if (BodySourceManager != null)
        {
            _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
            if (_BodyManager == null)
            {
                debugger.Log("No _BodyManager", null, LogType.Log);
                return;
            }
        }
        Kinect.Body[] data = _BodyManager.GetData();
        if (data != null)
        {
            foreach (var body in data)
            {
                if (body == null)
                {
                    continue;
                }

                if (body.IsTracked)
                {
                    Kinect.Joint leftHand = body.Joints[Kinect.JointType.HandLeft];
                    Kinect.Joint rightHand = body.Joints[Kinect.JointType.HandRight];






                    if (leftHand != null)
                    {
                        lastLeftHand = leftHand;
                        bottomHand.transform.position = new Vector3(leftHand.Position.X - 1.0f, leftHand.Position.Y, 2.0f - leftHand.Position.Z);
                    }
                    if (rightHand != null)
                    {
                        lastRightHand = rightHand;
                        topHand.transform.position = new Vector3(rightHand.Position.X -1.0f, rightHand.Position.Y, 2.0f - rightHand.Position.Z);
                    }
                    if ( leftHand != null && rightHand != null)
                    {

                        Vector3 batDir = topHand.transform.position - bottomHand.transform.position;
                        debugger.Log("batDir :" + batDir.x + "," + batDir.y + "," + batDir.z , null, LogType.Log);
                        float step = 20 * Time.deltaTime;
                        //Vector3 newDir = Vector3.RotateTowards(Vector3.right, batDir, step, 0.0F);
                        //Debug.DrawRay(transform.position, batDir * 50, Color.red,0.25f, false);

                        //var rotation = Quaternion.LookRotation(batDir.normalized);
                        //rotation *= originalRotationValue;
                        //transform.rotation = rotation;

                        //transform.rotation = originalRotationValue;
                        transform.rotation = Quaternion.FromToRotation(Vector3.up, batDir.normalized);


                        //transform.Rotate(rotateBatX, rotateBatY, rotateBatZ);
                        //Quaternion r_up = Quaternion.LookRotation(new Vector3(rotateBatX, rotateBatY, rotateBatZ), Vector3.up);
                        //Quaternion r_forward = Quaternion.LookRotation(new Vector3(rotateBatX, rotateBatY, rotateBatZ), Vector3.forward);
                        //Quaternion r_right = Quaternion.LookRotation(new Vector3(rotateBatX, rotateBatY, rotateBatZ), Vector3.right);
                        //Quaternion r = Quaternion.EulerRotation(rotateBatX, rotateBatZ, rotateBatY);

                        //transform.eulerAngles = Vector3.zero;
                        //transform.localRotation = r_up; // r_forward * r_up * r_right;
                        //transform.rotation = r_forward * r_right * r_up;

                        //float x = (leftHand.Position.X + rightHand.Position.X) / 2.0f;
                        //float y = (leftHand.Position.Y + rightHand.Position.Y) / 2.0f;
                        //float z = (leftHand.Position.Z + rightHand.Position.Z) / 2.0f;
                        //transform.position = new Vector3(x, y, z);

                        transform.position = bottomHand.transform.position;

                    }
                }
            }
        }

        if (steps_left > 0)
        {
            SwingMore();
        } else if (Input.GetKeyDown("m"))
        {
            steps_left = 13;
            SwingMore();
        }
        if (Input.GetKeyDown("j"))
        {
            transform.Translate(0, 0, -0.02f);
            debugger.Log("Grip Location: " + transform.position.ToString(), null, LogType.Log);
        }
        if (Input.GetKeyDown("l"))
        {
            transform.Translate(0, 0, 0.02f);
        }
        if (Input.GetKeyDown("i"))
        {
            transform.Translate(0, 0.02f, 0);
        }
        if (Input.GetKeyDown("k"))
        {
            transform.Translate(0, -0.02f, 0);
        }


    }

    void SwingMore()
    {
        transform.Rotate(new Vector3(0, -20f, 0));
        steps_left--;
        if (steps_left == 0)
        {
            transform.rotation = originalRotationValue;
        }
    }
}
