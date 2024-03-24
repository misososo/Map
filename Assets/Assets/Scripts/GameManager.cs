using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I = null;

    [SerializeField] List<Skill> allSkill;
    [SerializeField] List<Enemy> allEnemy;

    [SerializeField] Image blackScreen;

    int nowRoomId;//���݃v���C���[�����镔����ID

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
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        Time.fixedDeltaTime = Time.deltaTime;
    }

    //�t�F�[�h�C���̏���
    IEnumerator FadeIn()
    {
        var color = blackScreen.color;
        yield return new WaitForSeconds(0.2f);

        //���X�ɉ�ʂ��f��
        while (color.a >= 0)
        {
            color.a -= 0.1f;
            blackScreen.color = color;

            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
    }

    //�t�F�[�h�A�E�g�����Ă���V�[����؂�ւ���
    public IEnumerator ChangeScene(string sceneName)
    {
        blackScreen.gameObject.SetActive(true);

        var color = blackScreen.color;

        //�t�F�[�h�A�E�g������
        while (color.a <= 1)
        {
            color.a += 0.1f;
            blackScreen.color = color;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
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

    public int GetNowRoomId()
    {
        return nowRoomId;
    }

    public void SetRoomId(int i)
    {
        nowRoomId = i;
    }
}
