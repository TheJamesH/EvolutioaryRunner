using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleControler : MonoBehaviour {

    public int tic = 0;
    public int[] mTic = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] mStrength = new int[] { 16, 16, 16, 16, 16, 16 };
    public int compressDuration = 16;
    public float compressamount = .9f;

    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Line4;
    public GameObject Line5;
    public GameObject Line6;

    public GameObject cell1;
    public GameObject cell2;
    public GameObject cell3;
    public GameObject cell4;

    //get Components
    LineRenderer line1Renderer;
    LineRenderer line2Renderer;
    LineRenderer line3Renderer;
    LineRenderer line4Renderer;
    LineRenderer line5Renderer;
    LineRenderer line6Renderer;
    SpringJoint2D muscle1;
    SpringJoint2D muscle2;
    SpringJoint2D muscle3;
    SpringJoint2D muscle4;
    SpringJoint2D muscle5;
    SpringJoint2D muscle6;


    // Use this for initialization
    void Start ()
    {

    }

    public void setValues () {
        
        //getComponents
        line1Renderer = Line1.GetComponent<LineRenderer>();
        line2Renderer = Line2.GetComponent<LineRenderer>();
        line3Renderer = Line3.GetComponent<LineRenderer>();
        line4Renderer = Line4.GetComponent<LineRenderer>();
        line5Renderer = Line5.GetComponent<LineRenderer>();
        line6Renderer = Line6.GetComponent<LineRenderer>();
        foreach (SpringJoint2D spring in cell1.GetComponents<SpringJoint2D>())
        {
            if (spring.dampingRatio == .1f)
            {
                muscle1 = spring;
                muscle1.dampingRatio = 0;

            }
            if (spring.dampingRatio == .2f)
            {
                muscle2 = spring;
                muscle1.dampingRatio = 0;
            }
            if (spring.dampingRatio == .3f)
            {
                muscle3 = spring;
                muscle1.dampingRatio = 0;
            }
        }
        foreach (SpringJoint2D spring in cell2.GetComponents<SpringJoint2D>())
        {
            if (spring.dampingRatio == .4f)
            {
                muscle4 = spring;
                muscle4.dampingRatio = 0;

            }
            if (spring.dampingRatio == .5f)
            {
                muscle5 = spring;
                muscle5.dampingRatio = 0;
            }
        }
        foreach (SpringJoint2D spring in cell3.GetComponents<SpringJoint2D>())
        {
            if (spring.dampingRatio == .6f)
            {
                muscle6 = spring;
                muscle6.dampingRatio = 0;
            }
        }

        //set line width
        line1Renderer.widthMultiplier = mStrength[0] * .02f + .01f;
        line2Renderer.widthMultiplier = mStrength[1] * .02f + .01f;
        line3Renderer.widthMultiplier = mStrength[2] * .02f + .01f;
        line4Renderer.widthMultiplier = mStrength[3] * .02f + .01f;
        line5Renderer.widthMultiplier = mStrength[4] * .02f + .01f;
        line6Renderer.widthMultiplier = mStrength[5] * .02f + .01f;

        //set muscle spring frequencies
        muscle1.frequency = mStrength[0];
        muscle2.frequency = mStrength[1];
        muscle3.frequency = mStrength[2];
        muscle4.frequency = mStrength[3];
        muscle5.frequency = mStrength[4];
        muscle6.frequency = mStrength[5];


    }
	
	// Update is called once per frame
	void Update () {
        
        //update tic
        if(tic > 64 + compressDuration){
            tic = 0;
        }

        //set line locations
        Vector3 pos1 = cell1.transform.position;
        Vector3 pos2 = cell2.transform.position;
        Vector3 pos3 = cell3.transform.position;
        Vector3 pos4 = cell4.transform.position;

        pos1.z = 0;
        pos2.z = 0;
        pos3.z = 0;
        pos4.z = 0;

        line1Renderer.SetPosition(0, pos1);
        line1Renderer.SetPosition(1, pos2);
        line2Renderer.SetPosition(0, pos1);
        line2Renderer.SetPosition(1, pos3);
        line3Renderer.SetPosition(0, pos1);
        line3Renderer.SetPosition(1, pos4);
        line4Renderer.SetPosition(0, pos2);
        line4Renderer.SetPosition(1, pos3);
        line5Renderer.SetPosition(0, pos2);
        line5Renderer.SetPosition(1, pos4);
        line6Renderer.SetPosition(0, pos3);
        line6Renderer.SetPosition(1, pos4);

        //check for pulse
        if (tic == mTic[0])  muscle1.distance = muscle1.distance - compressamount;
        if (tic == mTic[0] + compressDuration) muscle1.distance = muscle1.distance + compressamount;
        if (tic == mTic[1]) muscle2.distance = muscle2.distance - compressamount;
        if (tic == mTic[1] + compressDuration) muscle2.distance = muscle2.distance + compressamount;
        if (tic == mTic[2]) muscle3.distance = muscle3.distance - compressamount;
        if (tic == mTic[2] + compressDuration) muscle3.distance = muscle3.distance + compressamount;
        if (tic == mTic[3]) muscle4.distance = muscle4.distance - compressamount;
        if (tic == mTic[3] + compressDuration) muscle4.distance = muscle4.distance + compressamount;
        if (tic == mTic[4]) muscle5.distance = muscle5.distance - compressamount;
        if (tic == mTic[4] + compressDuration) muscle5.distance = muscle5.distance + compressamount;
        if (tic == mTic[5]) muscle6.distance = muscle6.distance - compressamount;
        if (tic == mTic[5] + compressDuration) muscle6.distance = muscle6.distance + compressamount;



        tic++;
	}
}
