using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    bool isArrangement = false;

    protected List<Skill> dropSkills = new List<Skill>();
    int dropSkillsId = 0;
    //protected List<Skill> DropSkills = new List<Skill>();

    int id;

    bool isEnable = false;

    private void Update()
    {
        if(id != GameManager.I.GetNowRoomId() && isEnable)
        {
            Debug.Log("AAA");
            isEnable = false;
            DisableObject();
        }      
    }

    public virtual void ArrangementObject() { }

    public virtual void EnebleObject() { }

    public virtual void DisableObject() { }

    public void RemoveSprite()
    {
        sr.sprite = null;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int i)
    {
        id = i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isEnable = true;

            if(isArrangement)
            {
                EnebleObject();
            }
            else
            {
                isArrangement = true;
                ArrangementObject();
            }
        }
        else if(collision.gameObject.CompareTag("Skill"))
        {
            Skill skill = collision.GetComponent<Skill>();

            dropSkills.Add(skill);
            dropSkills[dropSkills.Count - 1].SetId(dropSkillsId);
            dropSkillsId++;
            dropSkills[dropSkills.Count - 1].RemoveDropSkillsCallBack(RemoveDropSkill);

            Debug.Log(dropSkills.Count);
        }
    }

    void RemoveDropSkill(int index)
    {
        Debug.Log("SSSSS");
        for(int i = 0; i < dropSkills.Count; ++i)
        {
            if(index == dropSkills[i].GetId())
            {
                dropSkills.RemoveAt(i);
            }
        }
    }
}
