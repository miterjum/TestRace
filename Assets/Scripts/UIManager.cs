using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private PlayerMove1 player;

    [Header("GameStartUI")]
    [SerializeField] private CanvasGroup gameStartUI;
    [Header("GameplayUI")]
    [SerializeField] private CanvasGroup gameplayUI;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private CanvasGroup nitro;
    [SerializeField] public Slider slider;
    [SerializeField] private CanvasGroup notwin;
    //[SerializeField] private Slider slid;
    [Header("GameRestartUI")]
    [SerializeField] private CanvasGroup gameRestartUI;
    [SerializeField] private TextMeshProUGUI inforText;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove1>();
    }
    public void UpdateTime(int timeInSecond)
    {
        timeText.SetText(string.Format("{0:00}:{1:00}", (int)(timeInSecond / 60), timeInSecond % 60));
    }
    public void UpdateCountTime(int t) 
    {
        countDownText.SetText("{0}",t);

    }
    private void Update()
    {

    }
    public void GamePlay()
    {
        gameStartUI.alpha = 0;
        gameStartUI.interactable = false;
        gameStartUI.blocksRaycasts = false;
        gameplayUI.alpha = 1;
        gameplayUI.blocksRaycasts = true;
        gameplayUI.interactable = true;

    }
    public void Lose()
    {
        gameplayUI.alpha = 0;
        gameplayUI.blocksRaycasts = false;
        gameplayUI.interactable = false;
        Time.timeScale = 0;
        gameRestartUI.alpha = 1;
        gameRestartUI.interactable = true;
        gameRestartUI.blocksRaycasts = true;
        inforText.SetText("GAMEOVER\nTRY AGAIN");
    }
    public void win()
    {
        gameplayUI.alpha = 0;
        gameplayUI.blocksRaycasts = false;
        gameplayUI.interactable = false;
        Time.timeScale = 0;
        gameRestartUI.alpha = 1;
        gameRestartUI.interactable = true;
        gameRestartUI.blocksRaycasts = true;
        inforText.SetText("GREAT\nTRY AGAIN");
    }
    public void delaynitro()
    {
        nitro.blocksRaycasts = false;
        nitro.interactable = false;
    }
    public void cannitro()
    {
        nitro.interactable = true;
        nitro.blocksRaycasts = true;
    }
    IEnumerator Delay()
    {
        notwin.alpha = 1;
        yield return new WaitForSeconds(1);
        notwin.alpha = 0;
    }
    public void delay()
    {
        StartCoroutine(Delay());
    }
}
