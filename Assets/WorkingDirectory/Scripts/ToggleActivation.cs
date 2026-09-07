using UnityEngine;

public class ToggleActivation : MonoBehaviour
{
    public void Start()
    {
        if (gameObject.tag == "Untagged")//When the app starts, if an object is untagged it will becmoe inactive regardless of how it was in the scene view.
        {
            gameObject.SetActive(false);
        }
    }
    public void ActivateMenu()//Activates the game object this script is attached to when called
    {
        gameObject.tag = "GameController";
        gameObject.SetActive(true);
    }

    public void DeActivateMenu()//Deactivates the game object this script is attached to when called
    {
        gameObject.tag = "Untagged";
        gameObject.SetActive(false);
    }
}
