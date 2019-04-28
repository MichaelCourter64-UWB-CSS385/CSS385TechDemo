using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Needs : MonoBehaviour {
    // Serial max values for needs
    [SerializeField] int MAX = 100;
    [SerializeField] int MIN = -100;

    // Uncomment and use if reverting from Animator Parameters as need counters
    //// Serial "need counters" for tracking creature's needs
    //[SerializeField] int hunger;
    //[SerializeField] int bladder;
    //[SerializeField] int happiness;
    //[SerializeField] int energy;

    // Booleans tracking what activity the creature is currently doing
    bool isRelievingSelf = false;
    bool isEating = false;
    bool isSleeping = false;
    bool isBeingPet = false;
    bool isWalking = false;

    // Reference to the Animator object which is this creature's FSM
    Animator CreatureFSM;
    
    // temp for storing values from the FSM Animator parameters
    int temp = 0;


    // Initializes Needs System
    // Gets a reference to this creature's FSM
    // Starts/uses InvokeReapeating for a fixed interval of calls to the
    // updateNeeds() function. (Set to occur every 30 seconds.)
    // 
    void Start () {
        CreatureFSM = GetComponent<Animator>();
        InvokeRepeating("updateNeeds", 0.0f, 30.0f);
	}
	
	// Updates the needs counters for the creature
    void updateNeeds()
    {
        
        if (!isEating)
        {
            temp = CreatureFSM.GetInteger("Hunger");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Hunger", temp);
        }
        if (!isRelievingSelf)
        {
            temp = CreatureFSM.GetInteger("Bladder");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Bladder", temp);
        }
        if (!isBeingPet)
        {
            temp = CreatureFSM.GetInteger("Happiness");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Happiness", temp);
        }
        if (!isSleeping)
        {
            temp = CreatureFSM.GetInteger("Energy");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Energy", temp);
        }
        // Note: Makes additional energy deduction if creature is moving
        if (isWalking)
        {
            temp = CreatureFSM.GetInteger("Energy");
            if (temp > (MIN - 1))
                temp--;
            CreatureFSM.SetInteger("Energy", temp);
        }
    }

    public int GetMAX()
    {
        return MAX;
    }

    public int GetMIN()
    {
        return MIN;
    }
}
