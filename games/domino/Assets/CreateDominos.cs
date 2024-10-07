using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDominos : MonoBehaviour
{
    public GameObject DominosPrefab;
    public float spacing = 1.1f; // Adjust spacing between dominoes
    public float knockForce = 10f; // Force to knock the first domino
    GameObject firstDomino;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = transform.position;

        for (int i = 0; i < 100; i++)
        {
          
            Vector3 position = startPos + transform.forward * i * spacing;

           
            GameObject domino = Instantiate(DominosPrefab, position, Quaternion.identity);

           
            if (i == 0)
            {
                firstDomino = domino;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (firstDomino != null)
            {
                Rigidbody rb = firstDomino.GetComponent<Rigidbody>();

               
                if (rb != null)
                {
                    rb.AddForce(transform.forward * knockForce, ForceMode.Impulse);
                }
            }
        }
    }
}
