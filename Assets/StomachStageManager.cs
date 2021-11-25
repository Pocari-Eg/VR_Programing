using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachStageManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] StomachStage;
    [SerializeField]
    GameObject StartPosition;

    [SerializeField]
    GameObject StartDialog;

    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject TelePortObj;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StageOn()
    {

        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.GetComponent<CharacterController>().center = new Vector3(0, -1.0f, 0);
        Player.GetComponent<CharacterController>().radius = 0.5f;
        Player.GetComponent<CharacterController>().height = 3;

        Player.GetComponent<ForceMove>().OnChangePlayerMode(2);
        Player.transform.position = StartPosition.transform.position;
        for (int i = 0; i < StomachStage.Length; i++)
        {
            StomachStage[i].SetActive(true);
        }
        TelePortObj.SetActive(false);

    }

    public void StageStart()
    {

        StartDialog.SetActive(false);
        TelePortObj.SetActive(true);
        Player.GetComponent<ForceMove>().OnChangePlayerMode(1);
        GameManager.instasnce.UIControllOff();
      

    }
    public void StageOff()
    {
        for (int i = 0; i < StomachStage.Length; i++)
        {
            StomachStage[i].SetActive(false);
        }
        Player.GetComponent<CapsuleCollider>().enabled = false;
    }

    public Vector3 GetStartPosition()
    {
        return StartPosition.transform.position;
    }
}
