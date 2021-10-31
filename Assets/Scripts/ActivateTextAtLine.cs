using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText; //NPC dialogue
    public int startLine; //new starting line
    public int endLine; //new ending line
    public TextBoxManager theTextBox; //shares text manager "TextBoxManager"
    public bool destroyWhenActivated; //remove textbox


	// Use this for initialization
	void Start () {

        theTextBox = FindObjectOfType<TextBoxManager>();
        //find text box manager	game object	
	}
	
    void OnTriggerEnter(Collider other) //on collision with "NPC talk zone"
    {
        if(other.name == "Player")
        {
            theTextBox.ReloadScript(theText); //passes new "the text" text file
            theTextBox.currentLine = startLine; //sets textboxmanager currentline to new startline
            theTextBox.endAtLine = endLine; //sets textboxmanager endline to new endline
            theTextBox.EnableTextBox(); //enable text box 

            if(destroyWhenActivated) //if is ticked
            {
                //removes textbox , destroys NPC talk zone
                Destroy(gameObject);
          }
       }
    }
}
