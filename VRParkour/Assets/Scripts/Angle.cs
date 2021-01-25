using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    private Vector3 initpositionL;
    private Vector3 initpositionR;
    public float rotation = 0f;
    private Quaternion mRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            initpositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            initpositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        }

        Vector3 currentpositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 currentpositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        //left
        if ((currentpositionL.y > initpositionL.y) && (currentpositionR.y < initpositionR.y))
        {
            //rotation += rotateSpeed;
            rotation += ((currentpositionL.y - initpositionL.y) - (currentpositionR.y - initpositionR.y)) / 2 * 1.5f;

        }

        //right
        if ((currentpositionL.y < initpositionL.y) && (currentpositionR.y > initpositionR.y))
        {

            rotation -= (-(currentpositionL.y - initpositionL.y) + (currentpositionR.y - initpositionR.y)) / 2 * 1.5f;
        }

        mRotation = Quaternion.Euler(0, rotation, 0);
        this.transform.rotation = mRotation;
    }
}
