using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update

   public GameObject playerName;


    int namePos = 0;

    char AlphaBet;

    char[] UserName = new char[10];

 

    void Start()
    {

        Debug.Log((int)'z');
        AlphaBet = 'a';
        UserName[namePos] = AlphaBet;
        InputName();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName.active == true)
        {
            if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
            {

                AlphaBet = (char)((int)AlphaBet + 1);

                if ((int)AlphaBet == 123)
                {
                    AlphaBet = 'a';
                }
                UserName[namePos]= AlphaBet;
                InputName();
            }


        }
            


       }

    public void InputName()
    {
        string name = new string(UserName);

        playerName.GetComponent<Text>().text = name;
    }
    
    public void Next(GameObject button)
    {
        if (namePos < 9)
        {
            
            namePos += 1;
            AlphaBet = 'a';
            UserName[namePos] = AlphaBet;
            InputName();
            if (namePos == 9)
            {
                button.GetComponent<Button>().interactable = false;
                button.GetComponent<BoxCollider>().enabled = false;
            }
        }
        

    }
}
