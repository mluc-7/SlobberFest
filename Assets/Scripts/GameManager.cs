using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public SlobberMovement m1;
    public SlobberMovement m2;
    public SlobberMovement m3;
    public SlobberMovement m4;
    public SlobberMovement m5;
    public SlobberMovement m6;
    public SlobberMovement m7;
    public SlobberMovement m8;
    public int cakesEaten;
    public int cakesToEat;
    public GameObject winPanel; 

    public void Update()
    {
     

        if (m2.ateCake && m4.ateCake)
        {
            winPanel.SetActive(true);
        }
    }
}
