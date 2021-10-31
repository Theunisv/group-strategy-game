using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NumberOrderMinigameCS : MonoBehaviour
{

    private List<GameObject> buttons = new List<GameObject>();
    private List<GameObject> activeButtons = new List<GameObject>();

    private MiniGameManager _miniGameManager;
    
    private int nxtButton = 1;
    // Start is called before the first frame update
    void OnEnable()
    {
        _miniGameManager = GameObject.Find("MiniGames").GetComponent<MiniGameManager>();
        buttons.Clear();
        activeButtons.Clear();
        nxtButton = 1;
        //On load get all children under the parent
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Clone"))
            {
                Destroy(child.gameObject);
            }
            else
            {
                child.gameObject.SetActive(true);
                buttons.Add(child.gameObject);
            }
        }

        if (buttons.Count == 12)
        {
            Randomise();
            activeButtons.Clear();
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
                
            }

            foreach (GameObject button in buttons)
            {
                
                GameObject newButton = Instantiate(button, this.transform);
                newButton.GetComponent<Image>().enabled = true;
                newButton.GetComponent<Button>().enabled = true;
                newButton.SetActive(true);
                activeButtons.Add(newButton);
                
            }
        }
    }
    
    //Method to randomise the position of the children within the parent
    private void Randomise()
    {
        // For each spot in the array, pick
        // a random item to swap into that spot.
        Debug.Log("Randomising " + buttons.Count + " children");
        for (int i = 0; i < buttons.Count - 1; i++)
        {
            int j = Random.Range(i, buttons.Count);
            Transform temp = buttons[i].transform;
            buttons[i] = buttons[j];
            buttons[j] = temp.gameObject;
        }
        
        Debug.Log("Button Count =" + buttons.Count);
    }

    public void OnItemClick(int buttonNr)
    {
        if (buttonNr == nxtButton)
        {
            nxtButton++;
            foreach (var button in activeButtons)
            {
                if (button.name.Contains("Num" + buttonNr + "("))
                {
                    button.GetComponent<Button>().interactable = false;
                }

            }
        }
        else StartCoroutine(startReset());

        if (nxtButton == 13)
        {
            _miniGameManager.TaskWasSuccessful();
        }
    }

    private void ResetGame()
    {
        Randomise();
        activeButtons.Clear();
            
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            
            if (child.name.Contains("Clone"))
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject button in buttons)
        {
                
            GameObject newButton = Instantiate(button, this.transform);
            newButton.SetActive(true);
            newButton.GetComponent<Image>().enabled = true;
            newButton.GetComponent<Button>().enabled = true;
            activeButtons.Add(newButton);
        }

        nxtButton = 1;
    }

    private IEnumerator startReset()
    {
        _miniGameManager.TaskFailed();
        yield return new WaitForSeconds(2.0f);
        ResetGame();
    }

}
