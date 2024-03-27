using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPoint : MonoBehaviour
{
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("GoalText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            text.text = "AÉ{É^ÉìÇ≈éüÇÃäKëwÇ…êiÇﬁ";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = "";
        }
    }
}
