using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour
{
    public int countDownTime;
    public Text CountDisplay;

    private void Start()
    {
        StartCoroutine(CountDownToStart());
    }

    IEnumerator CountDownToStart()
    {
        while (countDownTime > 0)
        {
            CountDisplay.text = countDownTime.ToString();
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        CountDisplay.text = "GO!!!";
        TimerController.instance.BeginTimer();

        yield return new WaitForSeconds(1f);

        CountDisplay.gameObject.SetActive(false);
    }
}
