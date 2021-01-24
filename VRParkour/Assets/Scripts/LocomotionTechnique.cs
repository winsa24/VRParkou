using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionTechnique : MonoBehaviour
{
    // Please implement your locomotion technique in this script. 
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    [Range(0, 10)] public float translationGain = 0.5f;
    public GameObject hmd;
    [SerializeField] private float leftTriggerValue;    
    [SerializeField] private float rightTriggerValue;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool isIndexTriggerDown;

    public float speed = 0f;
    public float maxSpeed = 8f;
    public float rotateSpeed = 1.5f;
    public float maxrotateSpeed = 3f;

    public float rotation = 0f;
    private Quaternion mRotation;

    private Vector3 initpositionL;
    private Vector3 initpositionR;

    public GameObject levelText;
    private int Lflag = 0;
    private int Rflag = 0;

    /////////////////////////////////////////////////////////
    // These are for the game mechanism.
    public ParkourCounter parkourCounter;
    public string stage;
    
    void Start()
    {
        levelText.SetActive(true);
    }

    void Update()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Please implement your LOCOMOTION TECHNIQUE in this script :D.
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            initpositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Rflag += 1;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            initpositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Lflag += 1;
        }
        if(Rflag >=1 && Lflag <= 1)
        {
            levelText.SetActive(false);
        }
        

        //speed up
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            speed -= 0.5f;
        }
        //speed down
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            speed += 0.5f;
        }
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        if(speed < 0)
        {
            speed = 0;
        }
        

        //rotate speed up
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {            
            rotateSpeed -= 0.1f;
        }
        //speed down
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            rotateSpeed += 0.1f;
        }
        if (rotateSpeed > maxrotateSpeed)
        {
            rotateSpeed = maxrotateSpeed;
        }
        if (rotateSpeed < 1)
        {
            rotateSpeed = 1f;
        }

        Vector3 movement = this.transform.TransformDirection(0, 0, 0);

        Vector3 currentpositionL = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 currentpositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        movement.z += speed;



        //left
        if ((currentpositionL.y > initpositionL.y) && (currentpositionR.y < initpositionR.y))
        {
            //rotation += rotateSpeed;
            rotation += ((currentpositionL.y - initpositionL.y) - (currentpositionR.y - initpositionR.y)) / 2 * rotateSpeed;

        }
        //right
        if ((currentpositionL.y < initpositionL.y) && (currentpositionR.y > initpositionR.y))
        {
            //rotation -= rotateSpeed;
            rotation -= (- (currentpositionL.y - initpositionL.y) + (currentpositionR.y - initpositionR.y)) / 2 * rotateSpeed;

        }

        ////left-shift
        //if (OVRInput.GetDown(OVRInput.Button.Four))
        //{
        //    rotation += 40;
        //}
        ////right-shift
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    rotation -= 40;
        //}

        //work
        this.transform.position += movement.magnitude * hmd.transform.forward.normalized * 0.08f;
        //instead up&down
        //this.transform.position += movement.magnitude * hmd.transform.up * 0.1f;
        //Debug.DrawLine (Vector3.zero, new Vector3 (0, 1, 0), Color.red);

        mRotation = Quaternion.Euler(0, rotation, 0);
        this.transform.rotation = mRotation;



        //leftTriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, leftController);
        //rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, rightController);

        //if (leftTriggerValue > 0.95f && rightTriggerValue > 0.95f)
        //{
        //    if (!isIndexTriggerDown)
        //    {
        //        isIndexTriggerDown = true;
        //        startPos = (OVRInput.GetLocalControllerPosition(rightController) + OVRInput.GetLocalControllerPosition(rightController)) / 2;
        //    }
        //    offset = hmd.transform.forward.normalized *
        //            ((OVRInput.GetLocalControllerPosition(leftController) - startPos) +
        //             (OVRInput.GetLocalControllerPosition(rightController) - startPos)).magnitude;
        //    Debug.DrawRay(startPos, offset, Color.red, 0.2f);
        //}
        //else if (leftTriggerValue > 0.95f && rightTriggerValue < 0.95f)
        //{
        //    if (!isIndexTriggerDown)
        //    {
        //        isIndexTriggerDown = true;
        //        startPos = OVRInput.GetLocalControllerPosition(leftController);
        //    }
        //    offset = hmd.transform.forward.normalized *
        //             (OVRInput.GetLocalControllerPosition(leftController) - startPos).magnitude;
        //    Debug.DrawRay(startPos, offset, Color.red, 0.2f);
        //}
        //else if (leftTriggerValue < 0.95f && rightTriggerValue > 0.95f)
        //{
        //    if (!isIndexTriggerDown)
        //    {
        //        isIndexTriggerDown = true;
        //        startPos = OVRInput.GetLocalControllerPosition(rightController);
        //    }
        //    offset = hmd.transform.forward.normalized *
        //             (OVRInput.GetLocalControllerPosition(rightController) - startPos).magnitude;
        //    Debug.DrawRay(startPos, offset, Color.red, 0.2f);
        //}
        //else
        //{
        //    if (isIndexTriggerDown)
        //    {
        //        isIndexTriggerDown = false;
        //        offset = Vector3.zero;
        //    }
        //}
        //this.transform.position = this.transform.position + (offset) * translationGain;


        ////////////////////////////////////////////////////////////////////////////////
        // These are for the game mechanism.
        if (OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Four))
        {
            if (parkourCounter.parkourStart)
            {
                this.transform.position = parkourCounter.currentRespawnPos;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        // These are for the game mechanism.
        if (other.CompareTag("banner"))
        {
            stage = other.gameObject.name;
            parkourCounter.isStageChange = true;
        }
        else if (other.CompareTag("coin"))
        {
            parkourCounter.coinCount += 1;
            this.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
        }
        // These are for the game mechanism.

    }
}