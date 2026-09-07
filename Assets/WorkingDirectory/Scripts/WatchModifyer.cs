using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchModifyer : MonoBehaviour
{
    [SerializeField] public Color blu = new Color(0.118f, 0.384f, 0.753f, 1f);
    [SerializeField] public Color red = new Color(1.000f, 0.655f, 0.392f, 1f);
    [SerializeField] public Color wht = new Color(0.902f, 0.925f, 0.949f, 1f);
    [SerializeField] public Color mar = new Color(0.631f, 0.122f, 0.165f, 1f);
    [SerializeField] public Color yel = new Color(1.000f, 0.749f, 0.000f, 1f);
    [SerializeField] public Color gre = new Color(0.239f, 0.902f, 0.545f, 1f);
    [SerializeField] public Color fus = new Color(0.702f, 0.278f, 0.549f, 1f);
    [SerializeField] public Color blk = new Color(0.090f, 0.090f, 0.086f, 1f);
    [SerializeField] public Color bri = new Color(0.0f, 0.588f, 1.0f, 1.0f); //brightblue

    [SerializeField] public Tracker tracker;
   
    public void Start()
    {
        ApplyDefaultColors();
    }

    // Method to apply default colors
    private void ApplyDefaultColors()
    {
        UpdateWatch("BodyFuschia");
        UpdateWatch("BordFuschia");
        UpdateWatch("CrRiFuschia");
        UpdateWatch("StraFuschia");
        UpdateWatch("StLoFuschia");
        Debug.Log("Default colors applied to the watch model!");
    }

    public void UpdateWatch(string componentColor)//Updates the model, called when button on watch menu pressed
    {
        int[] materials = new int[0];//Selects which part of the model to apply the change to.
        switch (componentColor.Substring(0, 4))
        {
            case "Body":
                materials = new int[] { 0, 1, 2, 5, 6, 7 };
                break;
            case "Bord":
                materials = new int[] { 3 };
                break;
            case "CrRi":
                materials = new int[] { 8, 9 };
                break;
            case "Stra":
                materials = new int[] { 10 };
                break;
            case "StLo":
                materials = new int[] { 11 };
                break;
        }
        foreach (int i in materials)//Applying the color to the selected part of the model
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
                case "Black":
                    GetComponent<MeshRenderer>().materials[i].color = blk;
                    break;
                case "Bright":
                    GetComponent<MeshRenderer>().materials[i].color = bri;
                    break;
                case "Fuschia":
                    GetComponent<MeshRenderer>().materials[i].color = fus;
                    break;
            }
        }
    }
    public void recordWatchUpdate(string componentColor)//Converts the string passed to it into an action to be recorded by the Tracker
    {
        string feature = "";
        string colorCode = "";
        switch (componentColor.Substring(0, 4))
        {
            case "Body":
                feature = "Body";
                break;
            case "Bord":
                feature = "Border";
                break;
            case "CrRi":
                feature = "Crown Ring";
                break;
            case "Stra":
                feature = "Strap";
                break;
            case "StLo":
                feature = "Strap Lock";
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
        }
        //"ID,Product,Feature,ColorCode,Texture,MenuType,TimeSinceStart"
        tracker.recordAction("HOLO-WATCH," + feature + "," + colorCode + ",NA");
    }
}
