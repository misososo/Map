using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Skill : MonoBehaviour
{
    [SerializeField] SkillData skillData;
    SpriteRenderer sr;
    new string name;
    Sprite sprite;
    string info;

    int id = -1;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void EnableSkill(ref Bullet bullet){} 

    public virtual void DisableSkill(ref Bullet bullet){}

    public void SetStatus()
    {
        sr = GetComponent<SpriteRenderer>();
        name = skillData.GetName();
        sprite = skillData.GetSprite();
        sr.sprite = sprite;
        info = skillData.GetInfo();
    }

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetId()
    {
        return id;
    }

    public string GetInfo()
    {
        return info;
    }

    public void SetId(int i)
    {
        id = i;
    }

    private void OnDestroy()
    {
        
        removeDropSkills(id);
          
    }

    //コールバック関数
    public delegate void CallBackInt(int i);

    CallBackInt removeDropSkills;

    public void RemoveDropSkillsCallBack(CallBackInt callBack)
    {
        removeDropSkills = callBack;
    }

    public delegate void CallBackVoid();

    CallBackVoid removeRoomSkills;

    public void RemoveRoomSkillsCallBack(CallBackVoid callBack)
    {
        removeRoomSkills = callBack;
    }
}
