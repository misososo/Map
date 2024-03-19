using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SkillSlot : MonoBehaviour
{
    GameObject[] slot = new GameObject[8];
    Skill[] haveSkill;
    [SerializeField] int maxHaveSkill;
    int selectingNow = 0;
    Vector3 rot;
    [SerializeField] Vector3 rotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        int childIndex = 0;

        for(int i = 0; i < transform.childCount; ++i)
        {
            slot[i] = transform.GetChild(childIndex).gameObject;
        }

        haveSkill = new Skill[maxHaveSkill];
    }

    // Update is called once per frame
    void Update()
    {
        //rot = transform.eulerAngles;
        //rot += rotationAmount;
        //transform.eulerAngles = Vector3.Lerp(rot, rot + rotationAmount, 0.1f);
    }

    public Skill GetHaveSkill(int index)
    {
        return haveSkill[index];
    }

    public void SetHaveSkill(Skill skill)
    {
        haveSkill[selectingNow] = skill;
    }

    public void CheckInputLeftBUtton(InputAction.CallbackContext context)
    {
        
        if(context.performed)
        {
            selectingNow++;

            if(selectingNow >= maxHaveSkill)
            {
                selectingNow = 0;
            }

            Debug.Log(selectingNow);
        }
    }

    public void CheckInputRightBUtton(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            rot = transform.eulerAngles;
            //rot -= rotationAmount;
            transform.eulerAngles = Vector3.Lerp(rot, rot - rotationAmount, 0.1f);

            selectingNow--;

            if(selectingNow < 0)
            {
                selectingNow = maxHaveSkill - 1;
            }

            Debug.Log(selectingNow);
        }
    }

    public void CheckInputDownBUtton(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("TRASH");
        }
    }
}
