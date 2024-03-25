using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;
    [TextArea]
    [SerializeField] string info;

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetInfo()
    {
        return info;
    }
}
