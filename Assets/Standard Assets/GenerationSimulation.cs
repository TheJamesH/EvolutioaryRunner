using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSimulation : MonoBehaviour {

    public int tic = 0;
    public int state = 1;
    public float fitness;
    public int simulation_length = 10000;
    public int testCreature = 1;
    public int[,,] creaturesInfo;
    public GameObject creature;

    // Use this for initialization
    void Start() {
        
    }

    public void Init(int populationIn, int[,,] creaturesInfoIn)
    {
        testCreature = populationIn;
        creaturesInfo = creaturesInfoIn;

        //Spawn prefab, initalize to set parameters
        creature = (GameObject)Instantiate(Resources.Load("SquareCreature") as GameObject);
        creature.transform.position = new Vector3(0, 0, 0);

        int[] mTic = new int[6];
        for (int j = 0; j < 6; j++)
        {
                mTic[j] = creaturesInfo[testCreature, 0, j];
        }
        int[] mStrength = new int[6];
        for (int j = 0; j < 6; j++)
        {
            mStrength[j] = creaturesInfo[testCreature, 1, j];
        }
        MuscleControler mControl = creature.GetComponent<MuscleControler>();
        mControl.mTic = mTic;
        mControl.mStrength = mStrength;
        mControl.setValues();


        state = 1;
    }

    // Update is called once per frame
    void Update() {

        
        
        if (state == 1) {

            //update fitness based on x location
            
            fitness = creature.transform.FindChild("cell1").position.x * -1f *10f;
            if(fitness < 0)
            {
                fitness = 0;
            }

            if (tic > simulation_length)
            {
                print("Fitness of creature #" + testCreature + " = " + fitness);
                creaturesInfo[testCreature, 2, 0] = (int)fitness;
                Destroy(creature);
                tic = 0;
                state = 0;
                EvolutionaryGenerator egen = GetComponent<EvolutionaryGenerator>();
                egen.runGeneration();

            }
            
            tic++;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, Screen.width - 10, Screen.height - 10), "tic: " + tic);
        GUI.Label(new Rect(10, 20, Screen.width, Screen.height), "current Fitness: " + fitness);
    }


}

