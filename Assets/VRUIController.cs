using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VRUIController : MonoBehaviour
{
    private bool bPressBtn = false;
    private bool bClickBtn = false;
    private float pressedTime = 0.0f; // �ش� ��ư�� �ü�ó�� ���� ���� ��� �ð�
    public float selectedBtnTime = 5.0f; // �ش� ��ư�� Ŭ�� ���� �ð�
    void Init()
    {
        pressedTime = 0.0f;
        bPressBtn = false;
        bClickBtn = false;
    }
    void Start()
    {
        Init();
    }
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
        Debug.Log("OnUIClickUI");
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerClickHandler);
    }
    public void OnUIPointerExit()
    {
        Debug.Log("OnUIPointerExit UI");
        Init();
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerExitHandler);
    }
    public void OnUIPointerEnter()
    {
        Debug.Log("OnUIPointerEnter UI");
        bPressBtn = true;
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerEnterHandler);
    }
}
