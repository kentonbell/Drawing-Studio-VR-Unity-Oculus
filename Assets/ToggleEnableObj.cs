using UnityEngine;

public class ToggleEnableObj : MonoBehaviour
{

    public GameObject obj;
    public bool isEnabled;
    public char capitalLetterToPress;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obj.SetActive(isEnabled);
    }

    


    // Update is called once per frame
    void Update()
    {
        KeyCode keyToCheck = (KeyCode)System.Enum.Parse(typeof(KeyCode), capitalLetterToPress.ToString());

        if (Input.GetKeyDown(keyToCheck))
        {
                Debug.Log(capitalLetterToPress + " key pressed");


                if (isEnabled)
                {
                    obj.SetActive(false);
                    isEnabled = false;
                }
                else
                {
                    obj.SetActive(true);
                    isEnabled = true;
                }


            Debug.Log("Toggled object. Now active: " + obj.activeSelf);

        }

        

    }
}
