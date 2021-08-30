using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private Vector3 startMousePosition;
    [SerializeField] private Vector3 lastMousePosition;
    [SerializeField] private float playerInput;
    [SerializeField] private float threshhold;
    public float horizontal;
    public float vertical;
    public bool handbrake;
    private PlayerMove1 player;
    public List<check> check;
    public List<Speed> speed;
    private void Start()
    {
        player = GetComponent<PlayerMove1>();
        check = new List<check>();
    }
    private void Update()
    {
        PlayerMouseInput();
    }
    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        //horizontal = Input.GetAxis("Horizontal");
        //handbrake = (Input.GetAxis("Jump") != 0) ? true:false ;
        horizontal = playerInput;
    }
    public void PlayerMouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if ((lastMousePosition - Input.mousePosition).magnitude > threshhold)
            {
                playerInput = (lastMousePosition - Input.mousePosition).x < 0 ? -(lastMousePosition - Input.mousePosition).x / 30 : -(lastMousePosition - Input.mousePosition).x / 30;
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                if (playerInput != 0)
                {
                    playerInput = playerInput + Time.deltaTime * 5 * (playerInput < 0 ? 1 : -1);
                    if (playerInput < 0.1f && playerInput > -0.1f)
                    {
                        playerInput = 0;
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            playerInput = 0;
        }
        if (playerInput > 1)
        {
            playerInput = 1;
        }
    }
    public void add(Speed T)
    {

        speed.Add(T);
        player.torque += 250;
    }
    public void addcheck(check T)
    {
        check.Add(T);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lose"))
        {
            Debug.Log("lose");
            GameManager.instance.Lose();
        }
        else if (other.gameObject.CompareTag("Win"))
        {
            Debug.Log("win");
            GameManager.instance.Win();
        }
        else if (other.gameObject.CompareTag("won"))
        {
            if (GameManager.instance.won == check.Count)
            {
                GameManager.instance.Win();
            }
            else
            {
                GameManager.instance.uiManager.delay();
            }
        }
    }
}
