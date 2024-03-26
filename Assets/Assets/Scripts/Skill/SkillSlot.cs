using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] slot = new SpriteRenderer[8];
    [SerializeField] int slotMaxNum;
    [SerializeField] Sprite emptySlot;
    [SerializeField] Skill[] haveSkill = new Skill[8];
    
    int selectSlotNow = 0;
    
    Vector3 rot;
    [SerializeField] float rotationAmount;
    [SerializeField] int rotationFlame;
    bool isRoll = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < slotMaxNum; ++i)
        {
            if(haveSkill[i])
            {
                slot[i].sprite = haveSkill[i].GetSprite();
            }
        }

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
        //Debug.Log(haveSkill[selectSlotNow].name);
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
            selectSlotNow++;

            if(selectSlotNow >= slotMaxNum)
            {
                selectSlotNow = 0;
            }

            StartCoroutine(RollSkillSlot());
        }
    }

    public void CheckInputRightBUtton(InputAction.CallbackContext context)
    {
        if(context.performed && !isRoll)
        {
            selectSlotNow--;

            if(selectSlotNow < 0)
            {
                selectSlotNow = slotMaxNum - 1;
            }

            StartCoroutine(RollSkillSlot(-1));
        }
    }

    public Skill[] GetHaveSkills()
    {
        return haveSkill;
    }

    public void SetHaveSkills(Skill[] s)
    {
        haveSkill = s;
    }
}
