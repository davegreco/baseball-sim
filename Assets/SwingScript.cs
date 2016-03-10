using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class SwingScript : MonoBehaviour {
    public Quaternion originalRotationValue; // declare this as a Quaternion


    Kinect.Joint lastLeftHand;
    Kinect.Joint lastRightHand;

    int steps_left = 0;

    GuiDebugScript debugger;


    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject bottomHand;
    public GameObject topHand;

    public Vector3 lastBottomHandPosition;
    public Vector3 lastBatOrientation;


    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private bool righty = true  ;

    // Use this for initialization
    void Start () {
        originalRotationValue = transform.rotation; // save the initial rotation
        debugger = gameObject.AddComponent<GuiDebugScript>();
    }


    // Update is called once per frame
    void Update () {
        Kinect.Body body = GetBatterData();
        if (body == null) return;

        Kinect.Joint leftHand = body.Joints[Kinect.JointType.HandLeft];
        Kinect.Joint rightHand = body.Joints[Kinect.JointType.HandRight];

        

        if (leftHand != null)
        {
            // If we have a leftHand - then update
            lastLeftHand = leftHand;
            if (righty)
                bottomHand.transform.position = BodySourceView.GetVector3FromJoint(leftHand);  // new Vector3(leftHand.Position.X - 1.0f, 2.71828f * leftHand.Position.Y, 2.0f - leftHand.Position.Z);
            else
                topHand.transform.position = new Vector3(leftHand.Position.X + 1.0f, 2.71828f * leftHand.Position.Y, 2.0f - leftHand.Position.Z);


        }
        if (rightHand != null)
        {
            // If we have a rightHand - then update
            lastRightHand = rightHand;
            if (righty)
                topHand.transform.position = BodySourceView.GetVector3FromJoint(rightHand);
            else
                bottomHand.transform.position = new Vector3(rightHand.Position.X + 1.0f, 2.71828f * rightHand.Position.Y, 2.0f - rightHand.Position.Z);
        }

        if ( leftHand != null && rightHand != null)
        {
            // Compute the directional orientation of the bat by projecting from bottomHand through topHand
            Vector3 batDir = topHand.transform.position - bottomHand.transform.position;
            // Rotate the bat system to point in the correct direction
            transform.rotation = Quaternion.FromToRotation(Vector3.up, batDir.normalized);
            // Position the bat system at the location of the bottomHand
            transform.position = bottomHand.transform.position;

        }
    }

    Kinect.Body GetBatterData()
    {
        if (BodySourceManager != null)
        {
            _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
            if (_BodyManager == null)
            {
                debugger.Log("No _BodyManager", null, LogType.Log);
                return null;
            }
        }
        Kinect.Body[] data = _BodyManager.GetData();
        // Just returning the first body - will need to do better than this!
        if (data != null)
        {
            foreach (var body in data)
            {
                if (body == null)
                {
                    continue;
                }

                if (body.IsTracked)
                    return body;
            }
        }
        return null;
    }

    void SetBatterStance()
    {
        Kinect.Body body = GetBatterData();
        Kinect.Joint leftHand = body.Joints[Kinect.JointType.HandLeft];
        Kinect.Joint rightHand = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint hipLeft = body.Joints[Kinect.JointType.HipLeft];
        Kinect.Joint hipRight = body.Joints[Kinect.JointType.HipRight];

        Kinect.Joint spineBase = body.Joints[Kinect.JointType.SpineBase];

        if (hipRight != null && hipLeft != null && spineBase != null && leftHand != null)
            righty = (hipRight.Position.Z >= hipLeft.Position.Z && spineBase.Position.X <= leftHand.Position.X);


    }

}
