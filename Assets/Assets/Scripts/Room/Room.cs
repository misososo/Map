using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;
    protected bool isArrangement = false;

    public virtual void ArrangementObject() { }

    public virtual void EnebleObject() { }

    public virtual void DisableObject() { }

    public void RemoveSprite()
    {
        sr.sprite = null;
    }
}
