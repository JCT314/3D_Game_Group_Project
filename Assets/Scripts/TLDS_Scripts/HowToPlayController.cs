using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    // Start is called before the first frame update
    /*public SpriteRenderer up_key;
    public SpriteRenderer down_key;
    public SpriteRenderer left_key;
    public SpriteRenderer right_key;

    public SpriteRenderer[] key_array = new SpriteRenderer[4]; */

    public GameObject sphere;

    public TMP_Text instruction;
    //public Button repeat_button;
    private Button back_button;

    public float amount = 0.0f;
    public float speed = 2.0f;
    void Awake()
    {
        //Obtain Sprite Components of Direction Keys
        /*up_key = GameObject.Find("Arrow_up").GetComponent<SpriteRenderer>();
        down_key = GameObject.Find("Arrow_down").GetComponent<SpriteRenderer>();
        left_key = GameObject.Find("Arrow_left").GetComponent<SpriteRenderer>();
        right_key = GameObject.Find("Arrow_right").GetComponent<SpriteRenderer>();*/

        //Obtain Sphere to control
        sphere = GameObject.Find("Example_Sphere");

        //Obtain Instruction Text
        instruction = GameObject.Find("Instruction_Text").GetComponent<TMP_Text>();

        //Obtain buttons
        back_button = GameObject.Find("Back_Button").GetComponent<Button>();
        // Adding function to back button to go back to the main menu
        back_button.onClick.AddListener(() => SceneManager.LoadScene(1));
        //repeat_button = GameObject.Find("Repeat_Button").GetComponent<Button>();

        //For ease of acces, Sprites are placed in Array (U,D,L,R)
        /*key_array[0] = up_key;
        key_array[1] = down_key;
        key_array[2] = left_key;
        key_array[3] = right_key;*/

        //repeat_button.gameObject.SetActive(false);
    }
    // Update is called once per frame
    /* Main loop that controls the position of the sphere on main canvas*/
    void Update()
    {
        amount = amount + Time.deltaTime;
        if(amount < 1.0f)
        {
            sphere.transform.Translate(Vector3.up * Time.deltaTime * speed);
            //change_key_green(key_array[0]);
        }
        else if (amount < 2.0f)
        {
            //change_key_white(key_array[0]);
            sphere.transform.Translate(Vector3.down * Time.deltaTime * speed);
            //change_key_green(key_array[1]);
        }
        else if (amount < 3.0f)
        {
            //change_key_white(key_array[1]);
            sphere.transform.Translate(Vector3.left * Time.deltaTime * speed);
            //change_key_green(key_array[2]);
        }
        else if (amount < 5.0f)
        {
            //change_key_white(key_array[2]);
            sphere.transform.Translate(Vector3.right * Time.deltaTime * speed);
            //change_key_green(key_array[3]);
        }
        else if (amount < 6.0f)
        {
            //change_key_white(key_array[3]);
            sphere.transform.Translate(Vector3.left * Time.deltaTime * speed);
            //change_key_green(key_array[2]);
        }
        else
        {
            //change_key_white(key_array[2]);
            instruction.gameObject.SetActive(false);
            //repeat_button.gameObject.SetActive(true);
        }
    }

    /*This function resets the instruction loop (meant to be linked with button)*/
    public void reset()
    {
        amount = 0;
        //repeat_button.gameObject.SetActive(false);
        instruction.gameObject.SetActive(true);
    }

    /*Function changes Sprite to be green*/
    private void change_key_green(SpriteRenderer key)
    {
        key.color = new Color(0, 1, 0, 1);
    }

    /*Function changes Sprite to be White*/
    private void change_key_white(SpriteRenderer key)
    {
        key.color = new Color(1, 1, 1, 1);
    }
}
