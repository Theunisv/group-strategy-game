using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    float[] rotations = { 0,90,180,270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    int PossibleRots = 1;
    

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

                          
private void Start()
    {
        Debug.Log("START");

        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
        
        if(PossibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameManager.correctMove();
                Debug.Log("E");
            }

            else
            {
                Debug.Log("E2");
            }
        }

        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                gameManager.correctMove();
                Debug.Log("F");
            }

            else
            {
                Debug.Log("F2");
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MOUSEDOWN");

        transform.Rotate(new Vector3(0, 0, 90));

        if (PossibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
                Debug.Log("B1");
            }

            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
                Debug.Log("B2");
            }

            else
            {
               Debug.Log("B3");
            }
        }

        else
        {       
            if ((int)transform.eulerAngles.z == correctRotation[0] && isPlaced == false)

            {            
                isPlaced = true;
                gameManager.correctMove();
                Debug.Log("C1");
            }

            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
                Debug.Log("C2");
            }

            else
            {
                Debug.Log("C3");

                Debug.Log(isPlaced);

               
                Debug.Log(transform.eulerAngles.z);
                Debug.Log(correctRotation[0]);
            }
        }
    }
}
