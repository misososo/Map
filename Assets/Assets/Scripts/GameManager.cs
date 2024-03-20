using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I = null;

    [SerializeField] List<Skill> allSkill;
    [SerializeField] List<Enemy> allEnemy;

    private void Awake()
    {
        I = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        for(int i = 0; i < allSkill.Count; ++i)
        {
            allSkill[i].SetStatus();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.fixedDeltaTime = Time.deltaTime;
    }

    public Skill GetSkill(int index)
    {
        return allSkill[index];
    }

    public int GetSkillNum()
    {
        return allSkill.Count;
    }

    public Enemy GetEnemy(int index)
    {
        return allEnemy[index];
    }

    public int GetEnemyNum()
    {
        return allEnemy.Count;
    }
}
