using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Needs : MonoBehaviour {
    // Serial max values for needs
    [SerializeField] int MAX = 100;
    [SerializeField] int MIN = -100;

    // Serial "need counters" for tracking creature's needs
    [SerializeField] int hunger;
    [SerializeField] int bladder;
    [SerializeField] int happiness;
    [SerializeField] int energy;

    // Booleans tracking what activity the creature is currently doing
    bool isWalking = false;
    bool isEating = false;
    bool isRelievingSelf = false;
    bool isBeingPet = false;
    bool isSleeping = false;


    // Starts/uses InvokeReapeating for a fixed interval of calls to the
    // updateNeeds() function. (Set to occur every 15 seconds.)
    void Start () {
        InvokeRepeating("updateNeeds", 0.0f, 15.0f);
	}
	
	// Updates the needs counters for the creature
    void updateNeeds()
    {
        if (!isEating)
        {
            hunger--;
        }
        if (!isRelievingSelf)
        {
            bladder--;
        }
        if (!isBeingPet)
        {
            happiness--;
        }
        if (!isSleeping)
        {
            energy--;
        }
        if (isWalking)
        {
            energy--;
        }
    }
}
