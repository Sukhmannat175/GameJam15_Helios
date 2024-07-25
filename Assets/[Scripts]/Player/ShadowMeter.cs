using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowMeter : MonoBehaviour
{
    [SerializeField] private Image uiFill;

    public float maxValue;
    public float currentValue;
    public float rate;


    // Start is called before the first frame update

    private void Update()
    {
        currentValue -= rate/60;
        uiFill.fillAmount = Mathf.InverseLerp(0, maxValue, currentValue);

        if (currentValue <= 0) Debug.Log("U dead");
    }
  
}
