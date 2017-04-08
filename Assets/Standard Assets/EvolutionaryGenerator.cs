using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EvolutionaryGenerator : MonoBehaviour {

    public int state = 0;
    public int population = 10;
    public int closeMutate;
    public int randomMutate;
    int genNum = 0;
    public int testNum = 0;
    public int[,,] creaturesInfo;
    GenerationSimulation generationSim;

    int tic = 0;
    float curFitness = 0;

    // Use this for initialization
    void Start () {

        creaturesInfo = new int[population, 3, 6];
        for (int i = 0; i < population; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                int a = Random.Range(0, 64);
                int b = Random.Range(1, 16);
                creaturesInfo[i, 0, j] = a;
                creaturesInfo[i, 1, j] = b;
            }
            creaturesInfo[i, 2, 0] = 0;
        }

        genNum = 0;
        generationSim = GetComponent<GenerationSimulation>();
        print("GEN: " + genNum);
        runGeneration();

    }

    public void runGeneration() //runs an entire generation of tests
    {
        
        if (testNum < population)
        {
            runTest(testNum);
            testNum++;
        }
        else //end of generation
        {
            printGeneration();
            mutate();
            genNum++;
            testNum = 0;
            print("GEN: " + genNum);
            runGeneration(); //loop
        }
    }

    void runTest(int testNum) //runs a single test of a creature
    {
        generationSim.Init(testNum, creaturesInfo);
    }

    void printGeneration() //saves generation information into a text file
    {
        using (StreamWriter writetext = File.AppendText("ECR_data.txt"))
        {
            int totalFitness = 0;
            int maxFitness = 0;
            writetext.WriteLine("---------");
            writetext.WriteLine("Generation: " + genNum.ToString());
            for (int i = 0; i < population; i++)
            {
                writetext.WriteLine(i.ToString());
                for (int j = 0; j < 6; j++)
                {
                    writetext.Write(creaturesInfo[i, 0, j].ToString() + " ");
                }
                writetext.WriteLine("");
                for (int j = 0; j < 6; j++)
                {
                    writetext.Write(creaturesInfo[i, 1, j].ToString() + " ");
                }

                writetext.WriteLine("");
                writetext.WriteLine("Fitness = " + creaturesInfo[i, 2, 0].ToString());


                totalFitness += creaturesInfo[i, 2, 0];
                if (creaturesInfo[i, 2, 0] > maxFitness)
                {
                    maxFitness = creaturesInfo[i, 2, 0];
                }
            }

            writetext.WriteLine("--Stats--");
            writetext.WriteLine("Max Fitness = " + maxFitness);
            writetext.WriteLine("Avg Fitness = " + (totalFitness / population));
            writetext.WriteLine("---------");
            writetext.Close();
        }
    }

    // Creates a new generation by mutating/copying the previous generation
    void mutate()
    {
        //create new  int[,,] creaturesInfo;
        int[,,] tempcreatures = new int[population, 3, 6]; ;
        int[] odds = new int[population];
        int totalFitness = 0;
        int maxFitness = 0;
        int maxFitnessLoc = 0;

        //add total fitnesses
        for (int i = 0; i < population; i++)
        {
            if (creaturesInfo[i, 2, 0] > maxFitness)
            {
                maxFitness = creaturesInfo[i, 2, 0];
                maxFitnessLoc = i;
            }
            totalFitness += creaturesInfo[i, 2, 0];
            odds[i] = totalFitness;
        }

        //creature 0 is copy of best of previous generation
        for (int j = 0; j < 6; j++)
        {
            tempcreatures[0, 0, j] = creaturesInfo[maxFitnessLoc, 0, j];
            tempcreatures[0, 1, j] = creaturesInfo[maxFitnessLoc, 1, j];
        }
        //fill rest of generation by copying previous generation based on prabability
        for (int i = 1; i < population; i++)
        {
            int rndmFitness = Random.Range(0, totalFitness);
            int copyIndex = 0;
            for (int n = 0; n < population; n++) //find index of creature to copy
            {
                if (rndmFitness <= odds[n])
                {
                    copyIndex = n;
                }
                else break;
            }
            
            //copy values
            for (int j = 0; j < 6; j++)
            {
                //Mutate based on set odds 1
                int closeMutateOdds = Random.Range(0, 100);
                int randomMutateOdds = Random.Range(0, 100);
                if (randomMutateOdds <= randomMutate)
                {
                    int mutateval = Random.Range(0, 64);
                    tempcreatures[i, 0, j] = mutateval;
                }
                else if (closeMutateOdds <= closeMutate)
                {
                    int mutateval = creaturesInfo[maxFitnessLoc, 0, j];
                    if (closeMutateOdds % 2 == 0)
                    {
                        mutateval--;
                        if (mutateval < 0) //bounds check
                        {
                            mutateval += 2;
                        }
                    }
                    else
                    {
                        mutateval++;
                        if (mutateval > 64) //bounds check
                        {
                            mutateval -= 2;
                        }
                    }
                    tempcreatures[i, 0, j] = mutateval;
                }
                else
                {
                    tempcreatures[i, 0, j] = creaturesInfo[maxFitnessLoc, 0, j];
                }

                //Mutate based on set odds 2
                closeMutateOdds = Random.Range(0, 100);
                randomMutateOdds = Random.Range(0, 100);
                if (randomMutateOdds <= randomMutate)
                {
                    int mutateval = Random.Range(1, 16);
                    tempcreatures[i, 1, j] = mutateval;
                }
                else if (closeMutateOdds <= closeMutate)
                {
                    int mutateval = creaturesInfo[maxFitnessLoc, 1, j];
                    if (closeMutateOdds % 2 == 0)
                    {
                        mutateval--;
                        if (mutateval < 1) //bounds check
                        {
                            mutateval += 2;
                        }
                    }
                    else
                    {
                        mutateval++;
                        if (mutateval > 16) //bounds check
                        {
                            mutateval -= 2;
                        }
                    }
                    tempcreatures[i, 1, j] = mutateval;
                }
                else
                {
                    tempcreatures[i, 1, j] = creaturesInfo[maxFitnessLoc, 1, j];
                }
            }

        }

        creaturesInfo = tempcreatures;

    }

    // Update is called once per frame
    void Update () {

    }


}


