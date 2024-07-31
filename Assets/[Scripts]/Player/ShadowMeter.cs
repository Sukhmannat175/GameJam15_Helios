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
    public SpriteRenderer shadow;
    private Color color;

    private void Start()
    {
        color = shadow.color;
    }

    private void Update()
    {
        if (currentValue >= maxValue) currentValue = maxValue;

        currentValue -= rate/120;
        uiFill.fillAmount = Mathf.InverseLerp(0, maxValue, currentValue);

        color.a = currentValue / maxValue;
        shadow.color = color;

        if (currentValue <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }  
}
