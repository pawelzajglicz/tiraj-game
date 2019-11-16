using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravitySwitches : MonoBehaviour
{
    Text gravitiesText;
    int switchesLeft;

    void Start()
    {
        gravitiesText = GetComponent<Text>();
        gravitiesText.text = GameManager.GetInstance().remainedLifes.ToString();
    }


    public void AddSwitches(int switchesToAdd)
    {
        switchesLeft += switchesToAdd;
        gravitiesText.text = switchesLeft.ToString();
    }

    public void RemoveWitches(int amountToRemove)
    {
        switchesLeft -= amountToRemove;
        gravitiesText.text = switchesLeft.ToString();
    }

    public void SetSwitches(int newValue)
    {
        switchesLeft = newValue;
        if (gravitiesText != null)
        {
            gravitiesText.text = switchesLeft.ToString();
        }
    }

    public static GravitySwitches instance;
    public GravitySwitches menu;


    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<GravitySwitches>();
            menu = instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GravitySwitches getInstance()
    {
        return instance;
    }
}
