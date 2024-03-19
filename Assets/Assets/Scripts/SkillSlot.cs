using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SkillSlot : MonoBehaviour
{
    SpriteRenderer[] slot = new SpriteRenderer[8];
    [SerializeField] int slotMaxNum;
    [SerializeField] Sprite emptySlot;
    Skill[] haveSkill;
    
    int selectSlotNow = 0;
    
    Vector3 rot;
    [SerializeField] float rotationAmount;
    [SerializeField] int rotationFlame;
    bool isRoll = false;

    // Start is called before the first frame update
    void Start()
    {
        int childIndex = 0;

        for(int i = 0; i < transform.childCount; ++i)
        {
            slot[i] = transform.GetChild(childIndex).GetComponent<SpriteRenderer>();
            childIndex++;
        }

        haveSkill = new Skill[slotMaxNum];
        rot.z = rotationAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RollSkillSlot(float dir = 1)
    {
        isRoll = true;

        for(int i = 0; i < rotationFlame; ++i)
        {
            transform.Rotate(dir * rot);
            yield return null;
        }

        isRoll = false;
    }

    public Skill GetHaveSkill()
    {
        return haveSkill[selectSlotNow];
    }

    public void EntryHaveSkill(Skill skill)
    {
        haveSkill[selectSlotNow] = skill;
        slot[selectSlotNow].sprite = skill.GetSprite();
    }

    public void RemoveHaveSkill(ref Bullet bullet)
    {
        if (!haveSkill[selectSlotNow]) return;

        haveSkill[selectSlotNow].DisableSkill(ref bullet);
        haveSkill[selectSlotNow] = null;
        slot[selectSlotNow].sprite = emptySlot;
    }

    public void CheckInputLeftBUtton(InputAction.CallbackContext context)
    {
        
        if(context.performed && !isRoll)
        {
            //rot = transform.eulerAngles + rotationAmount;

            selectSlotNow++;

            if(selectSlotNow >= slotMaxNum)
            {
                selectSlotNow = 0;
            }

            StartCoroutine(RollSkillSlot());
            //Debug.Log(selectingNow);
        }
    }

    public void CheckInputRightBUtton(InputAction.CallbackContext context)
    {
        if(context.performed && !isRoll)
        {
            //rot = transform.eulerAngles;
            //rot -= rotationAmount;
            //transform.eulerAngles = Vector3.Lerp(rot, rot - rotationAmount, 0.1f);

            selectSlotNow--;

            if(selectSlotNow < 0)
            {
                selectSlotNow = slotMaxNum - 1;
            }

            StartCoroutine(RollSkillSlot(-1));
            //Debug.Log(selectingNow);
        }
    }

    public void CheckInputDownBUtton(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            //Debug.Log("TRASH");
        }
    }
}
