using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private Vector3 startMousePosition;
    [SerializeField] private Vector3 lastMousePosition;
    [SerializeField] private float playerInput;
    [SerializeField] private float threshhold;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool canRun;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Run();
    }
    public void Run()
    {
        if (canRun)
        {
            rb.velocity = new Vector3(transform.forward.x * movementSpeed, rb.velocity.y, transform.forward.z * movementSpeed);
        }
    }
}