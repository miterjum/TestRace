using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AwakeManager : MonoBehaviour
{
    public Listvehicle list;
    public GameObject toRotate;
    public float rotatespeed;
    public int vehiclePointer=0;
    public GameObject start;
    private GameObject childObject;
    private void Awake()
    {
        vehiclePointer = PlayerPrefs.GetInt("pointer");
        childObject = Instantiate(list.vehicle[vehiclePointer], start.transform.position, start.transform.rotation) as GameObject;
    }
    private void FixedUpdate()
    {
        toRotate.transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
        childObject.transform.rotation = toRotate.transform.rotation;
    }
    public void rightButton()
    {
        if (vehiclePointer < list.vehicle.Length - 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            childObject = Instantiate(list.vehicle[vehiclePointer], start.transform.position, start.transform.rotation);
        }
    }

    public void leftButton()
    {
        if (vehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            childObject = Instantiate(list.vehicle[vehiclePointer], start.transform.position, start.transform.rotation);
        }
    }
    public void Loadmap1()
    {
        SceneManager.LoadScene("LV1");
    }
    public void Loadmap2()
    {
        SceneManager.LoadScene("LV2");
    }
    public void Loadmap3()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void exit()
    {
        Application.Quit();
    }
}
