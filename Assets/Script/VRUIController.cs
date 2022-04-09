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

    [SerializeField]
    GameObject Guage;
    void Init()
    {
        pressedTime = 0.0f;
        bPressBtn = false;
        bClickBtn = false;
    }
    void Start()
    {
        Guage = GameObject.FindGameObjectWithTag("Guage");
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
        Guage.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }
    public void OnUIPointerExit()
    {
        Debug.Log("OnUIPointerExit UI");
        Init();
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerExitHandler);
        Guage.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
    }
    public void OnUIPointerEnter()
    {
        Guage.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        Debug.Log("OnUIPointerEnter UI");
        bPressBtn = true;
        PointerEventData data = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, data, ExecuteEvents.pointerEnterHandler);
    }
}
