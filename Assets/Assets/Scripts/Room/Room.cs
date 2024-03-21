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

    [SerializeField] BoxCollider2D roomCollider;
    [SerializeField] Vector3 colliderSizeInPlayer;
    [SerializeField] Vector3 colliderSizeOutPlayer;

    int id;

    bool isEnable = false;

    private void Update()
    {
        if(id != GameManager.I.GetNowRoomId() && isEnable)
        {
            isEnable = false;
            roomCollider.size = colliderSizeOutPlayer;
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
            roomCollider.size = colliderSizeInPlayer;

            if (isArrangement)
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

            if (skill.GetId() != -1) return;

            dropSkills.Add(skill);
            dropSkills[dropSkills.Count - 1].SetId(dropSkillsId);
            dropSkillsId++;
            dropSkills[dropSkills.Count - 1].RemoveDropSkillsCallBack(RemoveDropSkill);

            
        }
    }

    void RemoveDropSkill(int index)
    {
        Debug.Log(dropSkills.Count);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            if(index == dropSkills[i].GetId())
            {
                dropSkills.RemoveAt(i);
            }
        }
    }
}
