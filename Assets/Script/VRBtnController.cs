using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRBtnController : MonoBehaviour
{
    private bool bPressBtn = false;
    private bool bClickBtn = false;
    private float pressedTime = 0.0f;   // 해당 버튼에 시선처리 했을 때의  경과 시간
    public float selectedBtnTime = 5.0f;    // 해당 버튼을 클릭 기준 시간 

    void Init()
    {
        pressedTime = 0.0f;
        bPressBtn = false;
        bClickBtn = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();        
    }

    // Update is called once per frame
    void Update()
    {
        if (bPressBtn && !bClickBtn)
        {
            pressedTime += Time.deltaTime;
            if (selectedBtnTime < pressedTime)
            {
                bClickBtn = true;
                OnUIClick();
            }
        }
    }

    public void OnUIClick()
    {
        Debug.Log("OnPointerClick UI");
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerClickHandler);
    }

    public void OnPointerExit()
    {
        Debug.Log("OnPointerExit UI");
        Init();
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerExitHandler);
    }

    public void OnPointerEnter()
    {
        Debug.Log("OnPointerEnter UI");
        bPressBtn = true;
        Debug.Log(bPressBtn);
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerEnterHandler);
    }
}
