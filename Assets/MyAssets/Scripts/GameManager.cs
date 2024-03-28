using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I = null;

    [SerializeField] List<Skill> allSkill;
    [SerializeField] List<GameObject> allItem;
    [SerializeField] List<Enemy> allEnemy;

    [SerializeField] string nextScene;
    [SerializeField] Image blackScreen;
    [SerializeField] Image gameOverScreen;
    [SerializeField] Image gameCrearScreen;
    [SerializeField] float displayTime;

    [SerializeField] string stageName;
    [SerializeField] Text stageText;

    bool isGameCrear = false;

    int nowRoomId = -1;//現在プレイヤーがいる部屋のID

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
        stageText.text = stageName;
    }

    // Update is called once per frame
    void Update()
    {
        Time.fixedDeltaTime = Time.deltaTime;
    }

    //フェードインの処理
    IEnumerator FadeIn()
    {
        var color = blackScreen.color;
        yield return new WaitForSeconds(0.2f);

        //徐々に画面を映す
        while (color.a >= 0)
        {
            color.a -= 0.1f;
            blackScreen.color = color;

            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
    }

    //フェードアウトさせてからシーンを切り替える
    public IEnumerator ChangeScene(string sceneName)
    {
        blackScreen.gameObject.SetActive(true);

        var color = blackScreen.color;

        //フェードアウトさせる
        while (color.a <= 1)
        {
            color.a += 0.1f;
            blackScreen.color = color;

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator GameCrear()
    {
        gameCrearScreen.gameObject.SetActive(true);

        var color = gameCrearScreen.color;

        //徐々に画面を映す
        while (color.a <= 1)
        {
            color.a += 0.01f;
            gameCrearScreen.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(displayTime);
    }

    public IEnumerator GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);

        var color = gameOverScreen.color;

        //徐々に画面を映す
        while(color.a <= 1)
        {
            color.a += 0.01f;
            gameOverScreen.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        StartCoroutine(ChangeScene("Title"));
    }

    public string GetNextScene()
    {
        return nextScene;
    }

    public Skill GetSkill(int index)
    {
        return allSkill[index];
    }

    public int GetSkillNum()
    {
        return allSkill.Count;
    }

    public GameObject GetItem(int index)
    {
        return allItem[index];
    }

    public int GetItemNum()
    {
        return allItem.Count;
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

    public bool GetIsGameCrear()
    {
        return isGameCrear;
    }

    public void SetIsGameCrear(bool b)
    {
        isGameCrear = b;
    }
}
