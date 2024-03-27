using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    bool isArrangement = false;

    protected List<Skill> dropSkills = new List<Skill>();
    int dropSkillsId = 0;
    
    [SerializeField] BoxCollider2D roomCollider;
    [SerializeField] Vector3 colliderSizeInPlayer;
    [SerializeField] Vector3 colliderSizeOutPlayer;

    Tilemap roomTileMap;
    protected Tilemap screenTileMap;
    [SerializeField] protected Tile wall;
    Vector3Int roomPos;
    protected Vector3Int[] setWallPos = { new Vector3Int(1, 2, 0), new Vector3Int(1, -4, 0), new Vector3Int(-2, -1, 0), new Vector3Int(4, -1, 0) };
    enum Dir
    {
        up,
        down,
        left,
        right
    }

    int id;

    bool isEnable = false;

    private void Start()
    {
        roomTileMap = GameObject.Find("RoomTilemap").GetComponent<Tilemap>();
        screenTileMap = GameObject.Find("ScreenTilemap").GetComponent<Tilemap>();
    }

    protected virtual void Update()
    {
        //Debug.Log(screenTileMap.name);
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

    protected void CheckNextRoom()
    {
        for(int i = 0; i < setWallPos.Length; ++i)
        {
            if (!screenTileMap.HasTile(setWallPos[i])) continue;

                screenTileMap.SetTile(setWallPos[i], null);
        }
        
        if (!roomTileMap.HasTile(roomPos + Vector3Int.up))
        {
            //Debug.Log()
            screenTileMap.SetTile(setWallPos[(int)Dir.up], wall);
        }

        if(!roomTileMap.HasTile(roomPos + Vector3Int.down))
        {
            screenTileMap.SetTile(setWallPos[(int)Dir.down], wall);
        }

        if (!roomTileMap.HasTile(roomPos + Vector3Int.left))
        {
            screenTileMap.SetTile(setWallPos[(int)Dir.left], wall);
        }

        if (!roomTileMap.HasTile(roomPos + Vector3Int.right))
        {
            screenTileMap.SetTile(setWallPos[(int)Dir.right], wall);
        }
    }

    public void SetRoomPos(int posX, int posY)
    {
        roomPos.x = posX;
        roomPos.y = posY;
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

            CheckNextRoom();

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
        //Debug.Log(dropSkills.Count);

        for (int i = 0; i < dropSkills.Count; ++i)
        {
            if(index == dropSkills[i].GetId())
            {
                dropSkills.RemoveAt(i);
            }
        }
    }
}
