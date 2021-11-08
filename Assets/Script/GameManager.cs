using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject CellPrefeb;
    [SerializeField]
    GameObject WBCPrefeb;
    [SerializeField]
    GameObject Player;

    float SpawnTime = 3.5f;

    float[] random = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCell();
    }


    void SpawnCell()
    {
        if (SpawnTime > 0.0f)
        {
            SpawnTime -= Time.deltaTime;
        }
        else
        {
            RandomSpwan();
            SpawnTime = 3.5f;
        }
            
      }

    void RandomSpwan()
    {

        int x = Random.Range(1, 10);
        float ranX = Random.Range(-9.0f, 9.0f);
        float ranY = Random.Range(3.0f, 6.0f);
        float ranZ = Random.Range(-9.0f, 9.0f);
        if (x < 2)
        {

            Instantiate(WBCPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity);

        }
        else
        {
            Instantiate(CellPrefeb, new Vector3(Player.transform.position.x + ranX, Player.transform.position.y + ranY, Player.transform.position.z + ranZ), Quaternion.identity);
        }

    }
}


 


