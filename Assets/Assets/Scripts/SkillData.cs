using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] Sprite sprite;

    public int GetId()
    {
        return id;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
