using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] SkillData skillData;
    SpriteRenderer sr;
    new string name;
    Sprite sprite;

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

    public void SetId(int i)
    {
        id = i;
    }

    private void OnDestroy()
    {
        removeDropSkills(id);
    }

    //コールバック関数
    public delegate void CallBack(int i);

    CallBack removeDropSkills;

    public void RemoveDropSkillsCallBack(CallBack callBack)
    {
        removeDropSkills = callBack;
    }
}
