using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowTimerScript : MonoBehaviour
{


    [SerializeField] private Image uiFill;

    public int Duration;

    private int DurationRemaining;


    // Start is called before the first frame update
    private void Start()
    {
        Being(Duration);
    }

    private void Being(int Seconds)
    {
        DurationRemaining = Seconds;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (DurationRemaining >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, DurationRemaining);
            DurationRemaining--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        //what happens when the timer runs out
        print("Dawg You Fucking Died");
    }


  
}
