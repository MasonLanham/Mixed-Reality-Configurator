using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using MixedReality.Toolkit.UX;


public class Tracker : MonoBehaviour
{
    private string modelChosen;
    private string menuTypeChosen;
    private int respondentID;

    public ToggleActivation watchMenuToggle;
    public ToggleActivation shoeMenuToggle;
    public ToggleActivation watchToggle;
    public ToggleActivation shoeToggle;
    public ToggleActivation animalMenuToggle;

    public ToggleActivation penguinToggle;
    public ToggleActivation dogToggle;
    public ToggleActivation dragonToggle;
    public ToggleActivation horseToggle;
    public ToggleActivation tigerToggle;

    public ToggleActivation penguinFullSizeToggle;
    public ToggleActivation tigerFullSizeToggle;
    public ToggleActivation dragonFullSizeToggle;
    public ToggleActivation horseFullSizeToggle;
    public ToggleActivation dogFullSizeToggle;
    
    public ToggleActivation penguinCondensedToggle;
    public ToggleActivation dogCondensedToggle;
    public ToggleActivation dragonCondensedToggle;
    public ToggleActivation horseCondensedToggle;
    public ToggleActivation tigerCondensedToggle;

    private List<string> records;
    private float startTime;

    public void Start()
    {
        modelChosen = "Watch";
        menuTypeChosen = "FullSize";
        records = new List<String>();
        respondentID = getNextRespondentID();
    }
    private int getNextRespondentID()//Finds the maximum respondent ID previously recorded in configurator.csv, increments it by 1 and returns that.
    { 
        String line;
        String filePath = Path.Combine(Application.persistentDataPath, "configurator.csv");
        int lineID = -1;
        int maxID = 0;
        if (File.Exists(filePath) == true)
        {
            using (StreamReader fileStream = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                line = fileStream.ReadLine();
                while (line != null)
                {
                    for (int charIndex = 0; charIndex < line.Length; charIndex++)
                    {
                        if (line.Substring(charIndex, 1) == ",")
                        {
                            try
                            {
                                lineID = Int32.Parse(line.Substring(0, charIndex));
                            }
                            catch (FormatException e)
                            {
                                Debug.Log(String.Format(e.ToString()));
                            }
                            if (lineID > maxID)
                            {
                                maxID = lineID;
                            }
                        }
                    }
                    line = fileStream.ReadLine();
                }
            }
        }
        if (maxID < 1)//If there is no respondent ID greater than one (we assume all are positive integers), then we need to include the data labels as the first line
        {
            records.Add("ID,Product,Feature,ColorCode,Texture,MenuType,TimeSinceStart(seconds)");
        }
        return maxID + 1;
    }
    public void SetModel(string s)
    { 
        modelChosen = s;
    }
    public void SetMenuType(string s)
    {
        menuTypeChosen = s;
    }
    public string GetMenuType()
    {
        return menuTypeChosen;
    }
    public void activateConfigurator()//This is called when the respondent presses the play button. It starts timing them and activates the necessary objects and menus
    {
        startTime = Time.time;
        switch (modelChosen + menuTypeChosen)
        {
            case "WatchFullSize":
                watchMenuToggle.ActivateMenu();
                watchToggle.ActivateMenu();
                break;
            case "ShoeFullSize":
                shoeMenuToggle.ActivateMenu();
                shoeToggle.ActivateMenu();
                break;
            case "PlushFullSize" or "PlushCondensed":
                modelChosen = "Dog"; //This is here because dog is selected by default
                animalMenuToggle.ActivateMenu();
                dragonToggle.ActivateMenu();
                horseToggle.ActivateMenu();
                penguinToggle.ActivateMenu();
                tigerToggle.ActivateMenu();
                dogToggle.ActivateMenu();
                break;
        }
    }
    public void activateAnimalConfigurator() //This is called when the respondent submits which animal they want to configure. It activates the necessary objects and menus depending on their choice.
    {
        switch (modelChosen)
        { 
            case "Dog":
                animalMenuToggle.DeActivateMenu();
                //dogToggle.DeActivateMenu();
                dragonToggle.DeActivateMenu();
                horseToggle.DeActivateMenu();
                penguinToggle.DeActivateMenu();
                tigerToggle.DeActivateMenu();
                if(menuTypeChosen == "FullSize") { 
                    dogFullSizeToggle.ActivateMenu(); 
                }
                else 
                    dogCondensedToggle.ActivateMenu();
                break;
            case "Dragon":
                animalMenuToggle.DeActivateMenu();
                dogToggle.DeActivateMenu();
                //dragonToggle.DeActivateMenu();
                horseToggle.DeActivateMenu();
                penguinToggle.DeActivateMenu();
                tigerToggle.DeActivateMenu();
                if (menuTypeChosen == "FullSize")
                {
                    dragonFullSizeToggle.ActivateMenu();
                }
                else
                    dragonCondensedToggle.ActivateMenu();
                break;                
            case "Horse":
                animalMenuToggle.DeActivateMenu();
                dogToggle.DeActivateMenu();
                dragonToggle.DeActivateMenu();
                //horseToggle.DeActivateMenu();
                penguinToggle.DeActivateMenu();
                tigerToggle.DeActivateMenu();
                if (menuTypeChosen == "FullSize")
                {
                    horseFullSizeToggle.ActivateMenu();
                }
                else
                    horseCondensedToggle.ActivateMenu();
                break;
            case "Penguin":
                animalMenuToggle.DeActivateMenu();
                dogToggle.DeActivateMenu();
                dragonToggle.DeActivateMenu();
                horseToggle.DeActivateMenu();
                //penguinToggle.ActivateMenu();
                tigerToggle.DeActivateMenu();
                if (menuTypeChosen == "FullSize")
                {
                    penguinFullSizeToggle.ActivateMenu();
                }
                else
                    penguinCondensedToggle.ActivateMenu();
                break;
            case "Tiger":
                animalMenuToggle.DeActivateMenu();
                dogToggle.DeActivateMenu();
                dragonToggle.DeActivateMenu();
                horseToggle.DeActivateMenu();
                penguinToggle.DeActivateMenu();
                //tigerToggle.DeActivateMenu();
                if (menuTypeChosen == "FullSize")
                {
                    tigerFullSizeToggle.ActivateMenu();
                }
                else
                    tigerCondensedToggle.ActivateMenu();
                break;
        } 
    }
    public void recordAction(string action)//Called whenever the configurator needs to record data
    {
        records.Add(respondentID + ","  + action +"," + menuTypeChosen + "," + (Time.time - startTime));
    }
    public void writeFile()//Called when the respondent completes configuration. Writes the records lines to the end of the configurator.csv file.
    {
        String filePath = Path.Combine(Application.persistentDataPath, "configurator.csv");
        Debug.Log(filePath);
        File.AppendAllLines(filePath, records);
    }
    public void printState()//Debugging method
    {
        Debug.Log("Model Chosen: " + modelChosen + "\nMenu Type Chosen: " + menuTypeChosen);
    }
}
