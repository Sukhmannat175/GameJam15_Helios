using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShadowMeter : MonoBehaviour
{
    [SerializeField] private Image uiFill;

    public float maxValue;
    public float currentValue;
    public float rate;
    public float maxRate = 10;

    // Start is called before the first frame update

    private void Update()
    {
        if (currentValue >= maxValue) currentValue = maxValue;

        currentValue -= rate/120;
        uiFill.fillAmount = Mathf.InverseLerp(0, maxValue, currentValue);

        if (currentValue <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }  
}
