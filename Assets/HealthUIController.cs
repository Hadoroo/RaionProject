using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    public GameObject heartContainer;
    
    private float fiiledValue;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        fiiledValue = (float)GameController.Health;
        fiiledValue = fiiledValue / GameController.MaxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fiiledValue;
    }
}
