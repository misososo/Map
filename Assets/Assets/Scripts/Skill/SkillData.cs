using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
