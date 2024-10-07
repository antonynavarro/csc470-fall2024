using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePrefabRow : MonoBehaviour
{
    
    public GameObject prefab;
    GameObject[] row;
    // Start is called before the first frame update
    void Start()
    {
        int radius = 100;
        row = new GameObject[100];
        for (int i = 0; i<radius; i++)
        {
           
            Vector3 pos = new Vector3();
            pos.x = (radius - i) * Mathf.Cos(i); 
            pos.z = (radius - i) * Mathf.Sin(i);
            GameObject thing = Instantiate(prefab, pos, Quaternion.identity);
            row[i] = thing;
        

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
