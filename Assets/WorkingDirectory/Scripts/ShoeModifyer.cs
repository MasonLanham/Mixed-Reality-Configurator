using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeModifyer : MonoBehaviour
{
    [SerializeField] public Color blu = new Color(0.118f, 0.384f, 0.753f, 1f);
    [SerializeField] public Color red = new Color(1.000f, 0.655f, 0.392f, 1f);
    [SerializeField] public Color wht = new Color(0.902f, 0.925f, 0.949f, 1f);
    [SerializeField] public Color mar = new Color(0.631f, 0.122f, 0.165f, 1f);
    [SerializeField] public Color yel = new Color(1.000f, 0.749f, 0.000f, 1f);
    [SerializeField] public Color gre = new Color(0.239f, 0.902f, 0.545f, 1f); // Lux Green
    [SerializeField] public Color fus = new Color(0.702f, 0.278f, 0.549f, 1f);
    [SerializeField] public Color blk = new Color(0.090f, 0.090f, 0.086f, 1f);
    [SerializeField] public Color brn = new Color(0.545f, 0.271f, 0.075f, 1f); // Brown

    [SerializeField] public Tracker tracker;
    public void Start()
    {
        ApplyDefaultColors();
    }

    // Method to apply default colors
    private void ApplyDefaultColors()
    {
        UpdateShoe("laceOrange");
        UpdateShoe("meshOrange");
        UpdateShoe("capsOrange");
        UpdateShoe("innrOrange");
        UpdateShoe("soleOrange");
        UpdateShoe("strpOrange");
        UpdateShoe("ptchOrange");

        Debug.Log("Default colors applied to the shoe model!");
    }

    public void UpdateShoe(string componentColor)
    {
        int[] materials = new int[0];//Selecting the parts of the model that need to be updated
        switch (componentColor.Substring(0, 4))
        {
            case "lace":
                materials = new int[] { 0 };
                break;
            case "mesh":
                materials = new int[] { 1, 6 };
                break;
            case "caps":
                materials = new int[] { 2 };
                break;
            case "innr":
                materials = new int[] { 3 };
                break;
            case "sole":
                materials = new int[] { 4 };
                break;
            case "strp":
                materials = new int[] { 5 };
                break;
            case "ptch":
                materials = new int[] { 7 };
                break;
        }
        foreach (int i in materials)//Applying the new color to the correct part of the model
        {
            switch (componentColor.Substring(4))
            {
                case "Blue":
                    GetComponent<MeshRenderer>().materials[i].color = blu;
                    break;
                case "Red":
                    GetComponent<MeshRenderer>().materials[i].color = red;
                    break;
                case "White":
                    GetComponent<MeshRenderer>().materials[i].color = wht;
                    break;
                case "Maroon":
                    GetComponent<MeshRenderer>().materials[i].color = mar;
                    break;
                case "Yellow":
                    GetComponent<MeshRenderer>().materials[i].color = yel;
                    break;
                case "Green":
                    GetComponent<MeshRenderer>().materials[i].color = gre;
                    break;
                case "Orange":
                    GetComponent<MeshRenderer>().materials[i].color = fus;
                    break;
                case "Black":
                    GetComponent<MeshRenderer>().materials[i].color = blk;
                    break;
                case "Brown":
                    GetComponent<MeshRenderer>().materials[i].color = brn;
                    break;
            }
        }
    }
    public void recordShoeUpdate(string componentColor)//Converting the string passed to an action to be recorded by the Tracker
    {
        string feature = "";
        string colorCode = "";
        switch (componentColor.Substring(0, 4))
        {
            case "lace":
                feature = "Lace";
                break;
            case "mesh":
                feature = "Body";
                break;
            case "caps":
                feature = "Eyelets";
                break;
            case "innr":
                feature = "Inner Body";
                break;
            case "sole":
                feature = "Sole";
                break;
            case "strp":
                feature = "Body Stripes";
                break;
            case "ptch":
                feature = "Back Collar";
                break;
        }
        switch (componentColor.Substring(4))
        {
            case "Blue":
                colorCode = "#1e62C0";
                break;
            case "Red":
                colorCode = "#ffa764";
                break;
            case "White":
                colorCode = "#e6ecf2";
                break;
            case "Maroon":
                colorCode = "#a11f2a";
                break;
            case "Yellow":
                colorCode = "#ffbf00";
                break;
            case "Green":
                colorCode = "#3de68b";
                break;
            case "Orange":
                colorCode = "#b3478c";
                break;
            case "Black":
                colorCode = "#171716";
                break;
        }
        //"ID,Product,Feature,ColorCode,Texture,MenuType,TimeSinceStart"
        tracker.recordAction("HOLO-SHOE," + feature + "," + colorCode + ",NA");
    }
}
