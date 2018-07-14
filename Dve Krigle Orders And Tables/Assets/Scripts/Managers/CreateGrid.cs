using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour {

    public int columns;
    public int rows;
    public GameObject cell;

    float cell_Dim;
    float tempX, tempY = 0.0f;

	// Use this for initialization
	void OnEnable () {

        GameObject go = Instantiate(cell, Vector3.zero, Quaternion.identity) as GameObject;
        cell_Dim = go.GetComponent<SpriteRenderer>().bounds.size.x;
        Destroy(go.gameObject);

        tempX = -columns / 2 * cell_Dim + cell_Dim / 2;
        tempY = rows / 2 * cell_Dim - cell_Dim / 2;

        for (int i = 0; i < rows; i++)
        {            
            for (int j = 0; j < columns; j++)
            {
                Vector3 pos = new Vector3(tempX, tempY, 0);
                GameObject c = Instantiate(cell, this.transform, false) as GameObject;
                c.transform.localPosition = pos;
                tempX += cell_Dim;
            }
            tempY -= cell_Dim;
            tempX = -columns / 2 * cell_Dim + cell_Dim / 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
