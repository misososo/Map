using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    bool isArrangement = false;

    List<Skill> dropSkills = new List<Skill>();
    //protected List<Skill> DropSkills = new List<Skill>();

    int id;

    bool isEnable = false;

    private void Update()
    {
        if(id != GameManager.I.GetNowRoomId() && isEnable)
        {
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
            if(isArrangement)
            {
                isEnable = true;
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
            dropSkills[dropSkills.Count - 1].RemoveDropSkillsCallBack(RemoveDropSkill);

            Debug.Log(dropSkills.Count);
        }
    }

    void RemoveDropSkill(int i)
    {
        dropSkills.RemoveAt(i);
    }
}
