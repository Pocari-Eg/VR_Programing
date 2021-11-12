using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PipeStageManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] BonusStage;

    [SerializeField]
    GameObject Player;
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
        for (int i = 0; i < BonusStage.Length; i++)
        {
            BonusStage[i].SetActive(true);
        }
        Player.GetComponent<CapsuleCollider>().enabled = true;
    }
    public void StageOff()
    {
        for (int i = 0; i < BonusStage.Length; i++)
        {
            BonusStage[i].SetActive(false);
        }
        Player.GetComponent<CapsuleCollider>().enabled = false;
    }
}
