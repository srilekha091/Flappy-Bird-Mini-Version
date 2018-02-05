using UnityEngine;
using System.Collections;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolders;
   
    private float lastPipeX;

    private float distance = 6f;

//The min and max values of the pipes that should appear in the Camera screen.
    private float pipeMin = -1.5f;
    private float pipeMax = 2.4f;

	void Awake () {

        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

//Gettig each of pipe position and setting it in the min and max range.
        for(int i=0; i<pipeHolders.Length; i++)
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;
        for (int i =1; i<pipeHolders.Length; i++)
        {
            if (lastPipeX < pipeHolders[i].transform.position.x)
            {
                lastPipeX = pipeHolders[i].transform.position.x;
            }
        }
           	
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            Vector3 temp = target.transform.position;
            temp.x = lastPipeX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);

            target.transform.position = temp;

            lastPipeX = temp.x;
        }
    }
}
