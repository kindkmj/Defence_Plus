using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour 
{
    private Text lifeLabel;
    public int life=10;
    void Start ()
    {
        lifeLabel = GetComponent<Text>();
    }

    void Update()
    {
        lifeLabel.text = "Life :  " + (life);
    }

    public void minLife()
    {
        life--;
    }
 

}
