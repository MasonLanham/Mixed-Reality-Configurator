using System;
using UnityEngine;

public class StartPositioner : MonoBehaviour
{
    [SerializeField] public Transform previousTransform;
    [SerializeField] public Transform startTransform;
    [SerializeField] public float xOffset; //Each offset should be entered assuming the HoloLens is facing 0 degrees.
    [SerializeField] public float yOffset;
    [SerializeField] public float zOffset;
    [SerializeField] public float xRotation;
    [SerializeField] public float yRotation;
    [SerializeField] public float zRotation;

    // Start is called before the first frame update
    public void Start()
    {
        double prevYRot = ((previousTransform.localEulerAngles.y) * (Math.PI)) / 180; //Converting the rotation about the Y axis to radians
        double newX = xOffset * Math.Cos(prevYRot) + zOffset * Math.Sin(prevYRot);  //Calculating the new x position and y position based on the xOffset, zOffset, and previousTransform's yRotation
        double newZ = zOffset * Math.Cos(prevYRot) - xOffset * Math.Sin(prevYRot);
        startTransform.position = previousTransform.position + new Vector3((float)newX, yOffset, (float)newZ); //Set start transform to previous objects transform + offset
        startTransform.localEulerAngles = new Vector3(xRotation, previousTransform.localEulerAngles.y + yRotation, zRotation); //We only want the y roatation from the previous object
    }
    public void Reposition(string s)
    {
        switch (s)
        {
            case "Dog":
                xOffset = 0.35f;
                break;
            case "Dragon":
                xOffset = 0.4f;
                break;
            case "Horse":
                xOffset = 0.28f;
                break;
            case "Penguin":
                xOffset = 0.28f;
                break;
            case "Tiger":
                xOffset = 0.28f;
                break;
        }
        double prevYRot = ((previousTransform.localEulerAngles.y) * (Math.PI)) / 180;
        double newX = xOffset * Math.Cos(prevYRot) + zOffset * Math.Sin(prevYRot);
        double newZ = zOffset * Math.Cos(prevYRot) - xOffset * Math.Sin(prevYRot);
        startTransform.position = previousTransform.position + new Vector3((float)newX, yOffset, (float)newZ);
        startTransform.localEulerAngles = new Vector3(xRotation, previousTransform.localEulerAngles.y + yRotation, zRotation);
    }
}
