using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] SkillData skillData;
    SpriteRenderer sr;
    int id;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        SetStatus();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSkill(ref Bullet bullet)
    {
        Debug.Log(id);
    }

    public void DisableSkill(ref Bullet bullet)
    {
        Debug.Log(-id);
    }

    public void SetStatus()
    {
        sr = GetComponent<SpriteRenderer>();
        id = skillData.GetId();
        sprite = skillData.GetSprite();
        sr.sprite = sprite;
    }

    public int GetId()
    {
        return id;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
