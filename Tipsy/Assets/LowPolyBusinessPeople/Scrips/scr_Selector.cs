﻿using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class scr_Selector : MonoBehaviour {

    //public Renderer[] suits;
    
    private int pick = 0;
    private int count = 0;
    public List<GameObject> Suits;
    public List<GameObject> Heads;
    private Renderer oRenderer;
    
    


    // Use this for initialization
    void Start()
    {

        // populate list based on tags
        foreach (Transform child in transform)
        {


            if (child.tag == "MaleSuit")
                {
                    Suits.Add(child.gameObject);
                    //Debug.Log(child + " added");
                }

            if (child.tag == "MaleHead")
                {
                    Heads.Add(child.gameObject);
                    //Debug.Log(child + " added");
                }

           
        }

        //pick a suit
        pickSuit();
        // pick headType A/B
        pickHead();
   
    }

    

    // Function for picking suits
    void pickSuit()
        {
        pick = Random.Range(0, Suits.Count);
            count = 0;

            foreach (GameObject o in Suits)
            {

                if (count == pick)
                {
                    oRenderer = o.GetComponentInChildren<Renderer>();
                    oRenderer.enabled = true;
                }
                else
                {
                    oRenderer = o.GetComponentInChildren<Renderer>();
                    oRenderer.enabled = false;
                }
                count++;
            }
        }

   
   
    

    // Function for picking heads and hands will be picked to match based on choice here too.
    void pickHead()
    {



                // now pick a head // the choice here is important to remeber so that we can choose hair styles that suit.
                pick = Random.Range(0, Heads.Count);
                
                count = 0;

                foreach (GameObject o in Heads)
                {

                    if (count == pick)
                    {
                        oRenderer = o.GetComponentInChildren<Renderer>();
                        oRenderer.enabled = true;
                    }
                    else
                    {
                        oRenderer = o.GetComponentInChildren<Renderer>();
                        oRenderer.enabled = false;
                    }
                    count++;
                }
    }
}

