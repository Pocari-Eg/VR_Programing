using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
public void ButtonDisable(GameObject Button)
    {
        Button.GetComponent<Button>().interactable = false;
        Button.GetComponent<BoxCollider>().enabled = false;
    }
 
}
