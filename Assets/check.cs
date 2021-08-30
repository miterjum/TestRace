using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
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
            Destroy(gameObject);
            controller.addcheck(this);
        }
    }
}
