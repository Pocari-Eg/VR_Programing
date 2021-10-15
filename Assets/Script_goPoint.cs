using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_goPoint : MonoBehaviour
{
    public GameObject[] goPointList;

    public GameObject[] goPoints;
    public float rotSpeed = 2.0f;
    public float moveSpeed = 0.8f;
    private int curGoPointIdx = 0;
    private int numGoPoints = 0;
    private Transform curTransform;

    bool isGoing = true;
 
// Start is called before the first frame update
void Start()
    {
  
        goPointList = GameObject.FindGameObjectsWithTag("goPoint");
        curGoPointIdx = 0; // 현재 GoPoint의 인덱스
        curTransform = GetComponent<Transform>(); // 현재 Player의 Transform
        goPoints = GameObject.FindGameObjectsWithTag("goPoint"); // 지정한 GoPoints들
        numGoPoints = goPoints.Length; // GoPoints 총 개수. 
    }
    void Update()
    {
        Debug.Log(" 실행중");
        if (isGoing)
        {
            MovePlayer();
        }
    }
   void MovePlayer()
    {
        // Player가 가야할 방향 결정
        Vector3 goDirection = goPoints[curGoPointIdx].transform.position - curTransform.position;
        // Player가 바라볼 방향의 Rot값을 쿼터니언을 통해서 구함. 
        Quaternion goRoation = Quaternion.LookRotation(goDirection);
        // 해당 방향으로 회전
        curTransform.rotation = Quaternion.Slerp(curTransform.rotation, goRoation, Time.deltaTime * rotSpeed);
        // 해당 방향으로 이동
        curTransform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("goPoint"))
        {
            //curGoPointIdx++;
            //if (curGoPointIdx == numGoPoints)
            //{
            //    curGoPointIdx = 0;

            //}

            isGoing = false;
            Debug.Log("Trigger Enter : " + other.name);
        }
    }


}
