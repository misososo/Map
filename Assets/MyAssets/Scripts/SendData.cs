using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendData : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] SkillSlot skillSlot;
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SendDataForNextScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bullet GetBullet()
    {
        return bullet;
    }

    public SkillSlot GetSkillSlot()
    {
        return skillSlot;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void SendDataForNextScene(Scene next, LoadSceneMode mode)
    {
        SendData bulletStatus = GameObject.Find("BulletData").GetComponent<SendData>();
        Bullet newBullet = bulletStatus.GetBullet();
        SkillSlot newSkillSlot = bulletStatus.GetSkillSlot();
        Player newPlayer = bulletStatus.GetPlayer();

        newBullet.SetReflectNum(bullet.GetReflectNum());
        newBullet.SetPenetrationNum(bullet.GetPenetrationNum());
        newBullet.SetProductScale(bullet.GetProductScale());
        newBullet.SetDivisionNum(bullet.GetDivisionNum());
        newBullet.SetIsDivision(bullet.GetIsDivision());

        newSkillSlot.SetHaveSkills(skillSlot.GetHaveSkills());

        newPlayer.SetMaxHp(player.GetMaxHp());
        newPlayer.SetHp(player.GetHp());
        newPlayer.SetBomNum(player.GetBomNum());
        
        SceneManager.sceneLoaded -= SendDataForNextScene;
    }
}
