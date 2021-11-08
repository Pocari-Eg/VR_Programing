using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMove : MonoBehaviour
{
    public int viewMode = 0;  // forcemove = 0, lookat = 1
    public GameObject[] goPoints;
    public float rotSpeed = 2.0f;
    public float moveSpeed = 0.8f;
    private int curGoPointIdx = 0;
    private int numGoPoints = 0;
    private Transform curTransform;

    private CharacterController curCharacterController;


    private Camera curPlayerCamera;


    
    // Start is called before the first frame update
    void Start()
    {
        curGoPointIdx = 0; // 현재 GoPoint의 인덱스
        curTransform = GetComponent<Transform>();   // 현재 Player의 Transform
        goPoints = GameObject.FindGameObjectsWithTag("goPoint"); // 지정한 GoPoints들
        numGoPoints = goPoints.Length; // GoPoints 총 개수. 
        curCharacterController = GetComponent<CharacterController>();   // 현재 Player의 CharacterController 얻기
        curPlayerCamera = Camera.main; // 현재 Camera 얻기
    }

    // Update is called once per frame
    void Update()
    {
        if (viewMode == 0) MovePlayer();
        else if (viewMode == 1) LookAtMovePlayer();
        else if (viewMode == 2) ForwardMove();
    }

    void ForwardMove()
    {
        // 현재 바라보고 있는 Camera의 방향이 플레이어가 움직이는 방향 
        Vector3 ForwardDir = curPlayerCamera.transform.forward;

        // 해당 방향의 높이 값을 0으로 주어서 움직일때 높이와 상관 없는 방향성 제시
        ForwardDir.y = 0;
        ForwardDir.x = 0;

        // 현재 CharacterController를 SimpleMove를 통해서 이동 
        curCharacterController.SimpleMove(ForwardDir * moveSpeed);

    }
    void LookAtMovePlayer()
    {
        // 현재 바라보고 있는 Camera의 방향이 플레이어가 움직이는 방향 
        Vector3 ForwardDir = curPlayerCamera.transform.forward;

        // 해당 방향의 높이 값을 0으로 주어서 움직일때 높이와 상관 없는 방향성 제시
        ForwardDir.y = 0;
        

        // 현재 CharacterController를 SimpleMove를 통해서 이동 
        curCharacterController.SimpleMove(ForwardDir * moveSpeed);
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

    public void OnChangePlayerMode(int playerMode)
    {
        viewMode = playerMode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="GoPoint")
        {
            curGoPointIdx++;

            if (curGoPointIdx == numGoPoints)
            {
                curGoPointIdx = 0;
            }

            Debug.Log("Trigger Enter : " + other.name);
        }
    }

}
