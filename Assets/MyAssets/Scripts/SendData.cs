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
        if (SceneManager.GetActiveScene().name != "Title")
        {
            SceneManager.sceneLoaded += SendDataForNextScene;
        }
            

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            bullet.SetReflectNum(0);
            bullet.SetPenetrationNum(0);
            bullet.SetProductScale(1);
            bullet.SetDivisionNum(0);
            bullet.SetIsDivision(false);

            skillSlot.SetHaveSkills(new Skill[8]);

            player.SetMaxHp(3);
            player.SetHp(3);
            player.SetBomNum(1);
        }
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
        if (!GameObject.Find("BulletData")) return;

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
