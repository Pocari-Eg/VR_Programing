using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRBtnController : MonoBehaviour
{
    private bool bPressBtn = false;
    private bool bClickBtn = false;
    private float pressedTime = 0.0f;   // �ش� ��ư�� �ü�ó�� ���� ����  ��� �ð�
    public float selectedBtnTime = 5.0f;    // �ش� ��ư�� Ŭ�� ���� �ð� 

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
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerEnterHandler);
    }
}
