using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMove : MonoBehaviour
{
    public int viewMode ;  // forcemove = 0, lookat = 1
 
    public float rotSpeed = 2.0f;
    public float moveSpeed = 0.8f;


    private Transform curTransform;

    private CharacterController curCharacterController;


    private Camera curPlayerCamera;


    
    // Start is called before the first frame update
    void Start()
    {

        curTransform = GetComponent<Transform>();   // 현재 Player의 Transform
        curCharacterController = GetComponent<CharacterController>();   // 현재 Player의 CharacterController 얻기
        curPlayerCamera = Camera.main; // 현재 Camera 얻기
    }

    // Update is called once per frame
    void Update() {
        if (viewMode == 0) LookAtMovePlayer();
      //  else if (viewMode == 2) StandPlayer();
        else
        {

        }

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

        void StandPlayer()
    {


        // 현재 CharacterController를 SimpleMove를 통해서 이동
    
        curCharacterController.SimpleMove(Vector3.zero);

    }

    public void OnChangePlayerMode(int playerMode)
    {
        viewMode = playerMode;
    }

   
    public void debugpos()
    {
        Debug.Log(this.gameObject.transform.position);
    }


}
