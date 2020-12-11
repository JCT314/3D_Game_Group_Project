using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits_Scene : MonoBehaviour
{
    Button back_button;
    // Start is called before the first frame update
    void Start()
    {
        back_button = GameObject.Find("Button_Main_Menu").GetComponent<Button>();
        back_button.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
