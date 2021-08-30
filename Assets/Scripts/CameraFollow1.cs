using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    private GameObject Player;
    private PlayerMove1 RR;
    private GameObject cameralookAt, cameraPos;
    private float speed = 0;
    [Range(0, 50)] public float smothTime = 8;

    private void Awake()
    {
        
    }
    private void Start()
    {   
        Player = GameObject.FindGameObjectWithTag("Player");
        RR = Player.GetComponent<PlayerMove1>();
        cameralookAt = Player.transform.Find("camera lookAt").gameObject;
        cameraPos = Player.transform.Find("camera constraint").gameObject;

    }

    private void FixedUpdate()
    {
        follow();

    }
    private void follow()
    {
        speed = RR.KPH / smothTime;
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameralookAt.gameObject.transform.position);
    }

}