using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] ranged;
    public GameObject[] melee;
    public int currentRanged;
    public int currentmelee;

    private Transform player;
    public bool usingRanged = false, usingMelee = false;
    public bool scrollUp, scrollDown;

    public void Start()
    {
        currentRanged = 0;
        currentmelee = 0;
        SwitchRanged();

        player = GameObject.Find("Player").GetComponent<Transform>();
    }


    public void Update()
    {
        transform.position = player.position;
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("Runs");
            if(usingRanged)
            {
                scrollUp = true;
                SwitchRanged();
               
            }

            if(usingMelee)
            {
                scrollUp = true;
                SwitchMelee();
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (usingRanged)
            {
                scrollDown = true;
                SwitchRanged();

            }

            if (usingMelee)
            {
                scrollDown = true;
                SwitchMelee();
            }
        }



    }

    public void SwitchRanged()
    {
        //When the player scrolls up check if you at the max number of weapons, set currentweapon to 0 if max reached
        if (scrollUp)
        {
            if (currentRanged >= ranged.Length - 1)
            {
                currentRanged = 0;
                ranged[currentRanged].SetActive(true);
            }
            else 
            { 
                currentRanged++;
                ranged[currentRanged].SetActive(true);
            }
            scrollUp = false;
        }
        //When the player scrolls down check if you at the bottom, set currentweapon max if min reached
        if(scrollDown)
        {
            if (currentRanged == 0)
            {
                currentRanged = ranged.Length - 1;
                ranged[currentRanged].SetActive(true);
            }
            else 
            { 
                currentRanged--;
                ranged[currentRanged].SetActive(true);
            }
            scrollDown = false;
        }

        //set the other weapons off
        for (int i = 0; i < ranged.Length; i++)
        {
            if(i != currentRanged)
            {
                ranged[i].SetActive(false);
            }
        }


        //if player doesnt have the weapon unlocked dont let them use it

    }



    public void SwitchMelee()
    {
        if (currentmelee >= melee.Length)
        {
            ranged[currentmelee].SetActive(true);
        }
    }
}
