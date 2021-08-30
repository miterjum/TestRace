using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove1 : MonoBehaviour
{
    internal enum driveType { 
        frontwheeldrive,
        rearwheeldrive,
        allwheeldrive,
        caculateEnginePower
    }
    [SerializeField]
    private driveType drive;
    public float KPH;
    public float smoothTime;
    private controller control;
    public WheelCollider[] wheel=new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float torque;
    public float addforcevalue;
    public float radious = 6f;
    public float brakepower;
    private Rigidbody rig;
    public GameObject centerofMass;
    public float[] slip=new float[4];
    public float thrust;
    public bool canRun=false;
    public float nitrusValue;
    // Start is called before the first frame update
    void Start()
    {
        getobject();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        addownforce();
        animateWheels();
        move();
        steering();
        getFriction();
        //activateNitrus();


    }
    private void move()
    {
        if (canRun)
        {
            if (drive == driveType.allwheeldrive)
            {
                for (int i = 0; i < wheel.Length; i++)
                {
                    wheel[i].motorTorque = torque / 4;

                }
            }
            else if (drive == driveType.rearwheeldrive)
            {
                for (int i = 2; i < wheel.Length; i++)
                {
                    wheel[i].motorTorque = torque / 2;

                }
            }
            else
            {
                for (int i = 0; i < wheel.Length - 2; i++)
                {
                    wheel[i].motorTorque = torque / 2;

                }
            }
            if (GameManager.instance.brake)
            {
                wheel[3].brakeTorque = wheel[2].brakeTorque = brakepower;
            }
            else
            {
                wheel[3].brakeTorque = wheel[2].brakeTorque = 0;
            }
            KPH = rig.velocity.magnitude * 3.6f;
        }
        else
        {
            for (int i = 0; i < wheel.Length; i++)
            {
                wheel[i].motorTorque = 0;

            }
        }
    }

    private void steering()
    {
        if (control.horizontal > 0)
        { 
            wheel[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radious + 1.5f / 2) * control.horizontal * 0.75f);
            wheel[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radious - 1.5f / 2) * control.horizontal * 0.75f);
        }
        else if (control.horizontal < 0)
        {
            wheel[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radious - 1.5f / 2) * control.horizontal * 0.75f);
            wheel[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radious + 1.5f / 2) * control.horizontal * 0.75f);
        }
        else
        {
            wheel[0].steerAngle = 0;
            wheel[1].steerAngle = 0;
        }
    }

    void animateWheels()
    {
        Vector3 Wheelpos = Vector3.zero;
        Quaternion wheelRot = Quaternion.identity;
        
        for(int i = 0; i < 4; i++)
        {
            wheel[i].GetWorldPose(out Wheelpos, out wheelRot);
            wheelMesh[i].transform.position = Wheelpos;
            wheelMesh[i].transform.rotation = wheelRot;
        }
    }
    private void getobject()
    {
        control = GetComponent<controller>();
        rig = GetComponent<Rigidbody>();
        centerofMass = GameObject.Find("COM");
        rig.centerOfMass = centerofMass.transform.localPosition;
    }
    private void addownforce()
    {
        rig.AddForce(-transform.up * addforcevalue * rig.velocity.magnitude);

    }
    private void getFriction()
    {
        for(int i = 0; i < wheel.Length; i++)
        {
            WheelHit wheelHit;
            wheel[i].GetGroundHit(out wheelHit);
            slip[i] = wheelHit.forwardSlip;

        }
    }
    public void activateNitrus()
    {
        if (!GameManager.instance.boosting && nitrusValue <= 10)
        {
            nitrusValue += Time.deltaTime / 2;
        }
        else
        {
            nitrusValue -= (nitrusValue <= 0) ? 0 : Time.deltaTime;
        }

        if (GameManager.instance.boosting)
        {
            if (nitrusValue > 0)
            { 
                rig.AddForce(transform.forward * 5000);
            }
        
        }


    }
}
