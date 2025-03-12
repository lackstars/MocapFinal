using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBodyOutline : MonoBehaviour
{
    LineRenderer lineRenderer;
    public List<GameObject> jointsToConnect;
    int segmentsCount;
    //public int delay = 0;
    private List<Vector3[]> delayedPoints;
    private List<Vector3> simplePoints;
    
    void Start()
    {
        segmentsCount = jointsToConnect.Count;
        lineRenderer = GetComponent<LineRenderer>();
        
        //---- SIMPLE VERSION
        //Create points needed on the line from list of Joints To Connect
        simplePoints = new List<Vector3>();
        for (int i = 0; i < segmentsCount; i++)
        {
            Vector3 tempPoint = jointsToConnect[i].transform.position;
            simplePoints.Add(tempPoint);
        }
        //--------------------

        //---- VERSION WITH DELAY
        // //Create points needed on the line from list of Joints To Connect
        // delayedPoints = new List<Vector3[]>();
        // for (int i = 0; i < segmentsCount; i++) {
        //     // Create a buffer array of points according to the size of the delay
        //     Vector3[] tempPoints = new Vector3[delay+1];
        //     for(int j= 0; j<delay+1; j++) {
        //         tempPoints[j] = jointsToConnect[i].transform.position;
        //     }
        //     delayedPoints.Add(tempPoints);
        // }
        //--------------------
    }

    void Update()
    {
        //---- SIMPLE VERSION
        // Update positions of all the joints in the list
        for (int i = 0; i < segmentsCount; i++)
        {
            simplePoints[i] = jointsToConnect[i].transform.position;
        }
        //--------------------
        
        //---- VERSION WITH DELAY
        // //Update buffer of points according to the new position of the joints
        // for (int i = 0; i < jointsToConnect.Count; i++) {
        //     for(int j= delay; j>0; j--) {
        //         // Shift points of the array (discard oldest value and replace with previous one)
        //         delayedPoints[i][j] = delayedPoints[i][j-1];
        //     }
        //     delayedPoints[i][0] = jointsToConnect[i].transform.position;
        // }
        //--------------------

        UpdateLine();
    }

    void UpdateLine () {
        // Create an array of Vector3 points that will form the line
        Vector3[] pointsOnTheLine = new Vector3[segmentsCount];
        
        //---- SIMPLE VERSION
        for (int i = 0; i < segmentsCount; i++)
        {
            pointsOnTheLine[i] = simplePoints[i];
        }
        //--------------------
        
        //---- VERSION WITH DELAY
        // for (int i = 0; i < segmentsCount; i++)
        // {
        //     //Pick the points at the end of the buffer array
        //     pointsOnTheLine[i] = delayedPoints[i][delayedPoints[i].Length-1];
        // }
        //--------------------

        // Draw the line
        for (int i = 0; i < segmentsCount; i++)
        {
            float t = i / (float)segmentsCount;
            lineRenderer.SetVertexCount(segmentsCount);
            lineRenderer.SetPositions(pointsOnTheLine);
        }

    }
}
