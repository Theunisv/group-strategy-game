using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox; //links to a canvas panel that can be activated and de-activated
    public Text theText; //accesses "text" location from panel
    public TextAsset textFile; // stores the .txt file 
    public string[] textLines; //displays each line of the text file 
    public int currentLine; // stores current  number
    public int endAtLine; // stores end line number
    public PlayerControls player; //player
    public bool isActive; // bool to activate and disable text box
    public bool stopPlayerMovement; //bool to stop player movement, and resume movement
    private bool isTyping = false; //bool for text typing
    private bool cancelTyping = false; //bool to stop text typing
    public float typeSpeed; // speed for textscroll

    void Start()
    {

        player = FindObjectOfType<PlayerControls>(); //finds player controller

        if (textFile != null) //if there is a textfile there, then
        {
            textLines = (textFile.text.Split('\n')); //get textlines array, from textfile grab text and split into seperate lines
        }

        if (endAtLine == 0) //if endatline = 0, 
        {

            endAtLine = textLines.Length - 1; //go as far as textlines -1
        }

        if (isActive) // if isactive is ticked, then
        {
            EnableTextBox(); //enable text box
        }

        else //otherwise disable
        {
            DisableTextBox(); //disable
        }
    }

    void Update()
    {
        if (!isActive) //if isactive is not ticked, then do nothing. 
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return)) //if return is pressed, and

        {
            if (!isTyping) //if istyping is false, then
            {
                currentLine += 1; //skip to & display next line in array

                if (currentLine > endAtLine) //if current line is bigger than end line, then
                {
                    DisableTextBox(); //disable textbox
                }

                else //otherwise
                {
                    StartCoroutine(TextScroll(textLines[currentLine])); //starts text scrolling
                }
            }

            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0; //start with nothing, and add 1 letter untill sentence is complete.
        theText.text = ""; //set text to null in textbox
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) //as long as typing has not been cancelled, and letter value is less than lineoftext -1
        {
            theText.text += lineOfText[letter]; //looks at string of text
            letter += 1; //moving on to next letter
            yield return new WaitForSeconds(typeSpeed); //type speed

        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);  // activates panel
        isActive = true; // sets isactive to true, displays textbox

        if (stopPlayerMovement) //if stopmovemet is ticked, then
        {
         //   player.canMove = false; // set canMove inside playercontroller to false, disable movement
        }

        
        //        StartCoroutine(TextScroll(textLines[currentLine])); //starts text scrolling
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false); //disables panel
        isActive = false; //sets isactive to false, disables textbox
      //  player.canMove = true; //set canMove inside playercontroller to true, allows movement        
    }

    public void ReloadScript(TextAsset theText) //reloads new text file for NPCs
    {
        if (theText != null) //if the text is there, then
        {
            textLines = new string[1]; //creates new string array for textlines, then 
            textLines = (theText.text.Split('\n')); //get textlines array, from textfile grab text and split into seperate lines
        }
    }
}
