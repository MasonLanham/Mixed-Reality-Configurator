using UnityEngine;
using System;
using MixedReality.Toolkit.UX;

public class TigerModifier : MonoBehaviour
{
    [SerializeField] public Color blu = new Color(0.118f, 0.384f, 0.753f, 1f);
    [SerializeField] public Color red = new Color(1.000f, 0.655f, 0.392f, 1f);
    [SerializeField] public Color wht = new Color(0.902f, 0.925f, 0.949f, 1f);
    [SerializeField] public Color mar = new Color(0.631f, 0.122f, 0.165f, 1f);
    [SerializeField] public Color ylw = new Color(1.000f, 0.749f, 0.000f, 1f);
    [SerializeField] public Color grn = new Color(0.239f, 0.902f, 0.545f, 1f);
    [SerializeField] public Color fus = new Color(0.702f, 0.278f, 0.549f, 1f);
    [SerializeField] public Color blk = new Color(0.090f, 0.090f, 0.086f, 1f);

    [SerializeField] public Material plain;
    [SerializeField] public Material stripes;
    [SerializeField] public Material spots;
    [SerializeField] public Material checker;
    [SerializeField] public Material wave;

    [SerializeField] public ToggleCollection colorToggleCollection;
    [SerializeField] public ToggleCollection textureToggleCollection;
    [SerializeField] public Tracker tracker;

    [SerializeField] public int numberOfParts = 7;

    private string partChosen;
    private string[] parts;
    private string[] colors;
    private string[] colorHexaCodes;
    private string[] textures;
    private string[] colorsChosen;
    private string[] texturesChosen;

    public void Start()
    {
        partChosen = "Body";
        parts = new string[] { "Body", "Belly", "Stripes", "Mouth", "Eyes", "Ears", "Tail" };
        colors = new string[] { "wht", "blu", "red", "mar", "ylw", "grn", "fus", "blk" };
        colorHexaCodes = new string[] { "#e6ecf2", "#1e62c0", "#ffa764", "#a11f2a", "#ffbf00", "#3de68b", "#b3478c", "#171716" }; //must match the colors string list
        textures = new string[] { "Plain", "Stripes", "Spots", "Checker", "Wave" };
        colorsChosen = new string[numberOfParts];
        texturesChosen = new string[numberOfParts];
        for (int i = 0; i < numberOfParts; i++)
        {
            colorsChosen[i] = "wht";
            texturesChosen[i] = "Plain";
        }
    }
    public void setPartChosen(string s) { partChosen = s; }
    public void setColorChosen(string s)
    {
        colorsChosen[getPartIndex()] = s;
    }
    public void setTextureChosen(string s)
    {
        texturesChosen[getPartIndex()] = s;
    }
    private int getPartIndex() //returns the index of the partChosen in the parts string list
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (partChosen == parts[i])
            {
                return i;
            }
        }
        return -1;
    }
    private int getColorIndex() //returns the index of the color chosen for the current partChosen
    {
        string colorChosen = colorsChosen[getPartIndex()];
        for (int i = 0; i < colors.Length; i++)
        {
            if (colorChosen == colors[i])
            {
                return i;
            }
        }
        return -1;
    }
    private int getTextureIndex() //returns the index of the texture chosen for the current partChosen
    {
        string textureChosen = texturesChosen[getPartIndex()];
        for (int i = 0; i < textures.Length; i++)
        {
            if (textureChosen == textures[i])
            {
                return i;
            }
        }
        return -1;
    }
    public void updateMenuOnPartChange(string partChangedTo)
    {
        setPartChosen(partChangedTo);
        colorToggleCollection.SetSelection(getColorIndex());
        textureToggleCollection.SetSelection(getTextureIndex());
    }
    public void UpdateModel()
    {
        int[] materials = new int[0]; //The materials to be changed. needs to be independent of partIndex because some models have multiple materials that need to be changed by one selection
        int partIndex = getPartIndex();
        switch (partChosen)
        {
            case "Body":
                materials = new int[] { 2 };
                break;
            case "Belly":
                materials = new int[] { 1 };
                break;
            case "Stripes":
                materials = new int[] { 3 };
                break;
            case "Mouth":
                materials = new int[] { 4 };
                break;
            case "Eyes":
                materials = new int[] { 5 };
                break;
            case "Ears":
                materials = new int[] { 0 };
                break;
            case "Tail":
                materials = new int[] { 6 };
                break;
        }
        foreach (int i in materials)
        {
            switch (texturesChosen[partIndex])
            {
                case "Stripes":
                    GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(stripes); //For Textures Needs to be a material
                    break;
                case "Spots":
                    GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(spots);
                    break;
                case "Plain":
                    GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(plain);
                    break;
                case "Checker":
                    GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(checker);
                    break;
                case "Wave":
                    GetComponent<MeshRenderer>().materials[i].CopyPropertiesFromMaterial(wave);
                    break;
            }
        }
        foreach (int i in materials)
        {
            switch (colorsChosen[partIndex])
            {
                case "blu":
                    GetComponent<MeshRenderer>().materials[i].color = blu;
                    break;
                case "red":
                    GetComponent<MeshRenderer>().materials[i].color = red;
                    break;
                case "wht":
                    GetComponent<MeshRenderer>().materials[i].color = wht;
                    break;
                case "mar":
                    GetComponent<MeshRenderer>().materials[i].color = mar;
                    break;
                case "ylw":
                    GetComponent<MeshRenderer>().materials[i].color = ylw;
                    break;
                case "grn":
                    GetComponent<MeshRenderer>().materials[i].color = grn;
                    break;
                case "fus":
                    GetComponent<MeshRenderer>().materials[i].color = fus;
                    break;
                case "blk":
                    GetComponent<MeshRenderer>().materials[i].color = blk;
                    break;
            }
        }
        tracker.recordAction("HOLO-TIGER," + partChosen + "," + colorHexaCodes[getColorIndex()] + "," + texturesChosen[partIndex]);
    }
}
