using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int won;
    public static GameManager instance;
    public int curTime;
    public bool boosting;
    public UIManager uiManager;
    public int time = 3;
    private float startposition = 220f, endposition = -40f;
    public GameObject needo;
    private float desiredpos;
    private float vehiclespeed;
    public PlayerMove1 player;
    private Coroutine countTimeCoroutine;
    private Coroutine countdown;
    public GameObject startPosition;
    public Listvehicle list;
    public bool brake;
    public float timedelay=2f;
    public float times = 5f;
    public Transform rotations;
    private void Awake()
    {
        Instantiate(list.vehicle[PlayerPrefs.GetInt("pointer")], startPosition.transform.position, startPosition.transform.rotation);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove1>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove1>();
        uiManager = GetComponent<UIManager>();
    }
    private void Updatespeed()
    {
        vehiclespeed = player.KPH;
        desiredpos = startposition - endposition;
        float temp = vehiclespeed / 180;
        needo.transform.eulerAngles = new Vector3(0, 0, startposition - temp * desiredpos);
        uiManager.slider.value = player.nitrusValue;
        if (player.nitrusValue <= 0)
        {
            boosting = false;
            StartCoroutine(DelayNitro());
        }
    }
    IEnumerator CountTime()
    {
        uiManager.UpdateTime(curTime);
        while (curTime > 0)
        {
            curTime--;
            yield return new WaitForSeconds(1);
            uiManager.UpdateTime(curTime);
        }
        Lose();
    }
    IEnumerator CountdownTime() 
    {
        uiManager.UpdateCountTime(time);
        while(time > 0)
        {
            time--;
            yield return new WaitForSeconds(1);
            uiManager.UpdateCountTime(time);
        }
        StartGame();
    }
    public void Handbrake(bool add)
    {
        brake = add;
    }
    private void FixedUpdate()
    {
        Updatespeed();
        player.activateNitrus();
    }
    private void Start()
    {
        StartCoroutine(CountdownTime());

    }
    public void StartGame()
    {  
        player.canRun = true;
        StartCoroutine(CountTime());
        uiManager.GamePlay();
    }
    public void RestartGame(int scene)
    {
       SceneManager.LoadScene(scene);
    }
    public void Lose()
    {
        uiManager.Lose();
    }
    public void Win()
    {
        uiManager.win();
    }
    public void boost(bool add)
    {
        if (add)
        {
            if (boosting) { boosting = false; }
            else { boosting = true; }
        }
    }
    IEnumerator DelayNitro()
    {
        uiManager.delaynitro();
        yield return new WaitForSeconds(timedelay);
        uiManager.cannitro();
    }
    public void transposition()
    {
        if(!brake && player.KPH <= 1) 
        {
            times -=Time.deltaTime;
            if (times <= 0)
            {
                player.transform.rotation = startPosition.transform.rotation;
                times = 5f;
            }
        }
    }
}
