using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Speed : MonoBehaviour
{
    public controller controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controller.add(this);
            Destroy(gameObject);

        }
    }

}
