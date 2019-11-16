using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public Menu menu;


    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<Menu>();
            menu = instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Menu getInstance()
    {
        return instance;
    }
    

}
