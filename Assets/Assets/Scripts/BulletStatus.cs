using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletStatus : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] SkillSlot skillSlot;

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

    public void SendDataForNextScene(Scene next, LoadSceneMode mode)
    {
        BulletStatus bulletStatus = GameObject.Find("BulletData").GetComponent<BulletStatus>();
        Bullet newBullet = bulletStatus.GetBullet();
        SkillSlot newSkillSlot = bulletStatus.GetSkillSlot();

        newBullet.SetReflectNum(bullet.GetReflectNum());
        newBullet.SetPenetrationNum(bullet.GetPenetrationNum());
        newBullet.SetProductScale(bullet.GetProductScale());
        newBullet.SetDivisionNum(bullet.GetDivisionNum());
        newBullet.SetIsDivision(bullet.GetIsDivision());

        newSkillSlot.SetHaveSkills(skillSlot.GetHaveSkills());
        newSkillSlot.SetSlot(skillSlot.GetSlot());
        newSkillSlot.SetSprite(newSkillSlot.GetSlot());

        SceneManager.sceneLoaded -= SendDataForNextScene;
    }
}
