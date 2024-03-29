using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public static bool Check = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if(collision.gameObject.name == "asd")
            SceneManager.LoadScene("2");
    }




}
