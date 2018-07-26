﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Signup : MonoBehaviour
{
    public InputField Username;
    public InputField EmailField;
    public InputField Password;
    public InputField ConfirmPassword;
    public Button Register;
    public Text info;
   // private string Username;
    //private string password;
    //private string confpassword;
    //private string email;
    private bool EmailValid = false;
    private bool valid = false;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Username.isFocused)
            {
                EmailField.Select();
            }
            if (EmailField.isFocused)
            {
                Password.Select();
            }
            if (Password.isFocused)
            {
                ConfirmPassword.Select();
            }
        }

        Register.onClick.AddListener(check);
    }

    bool isValidEmail(string mail)
    {
    // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(mail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
    }

    void check()
        {
            if (Username.text!=""&&Password.text!="" &&EmailField.text!=""&&ConfirmPassword.text!="")
            { 
                if(Password.text.Length < 6)
                {
                    info.text="password should be of atleast 6 char";
                }
                else if (!isValidEmail(EmailField.text))
                {
                    info.text="Email invalid";
                }
                else if(Password.text!=ConfirmPassword.text)
                {
                    info.text="passwords mismatch";
                }
                else
                {
                    if(!valid){
                        valid=true;
                        CallRegistration();
                    }
                }
            }
            else
            {
                info.text="Fill all boxes";
            }
        }
   

    public void CallRegistration()
    {
        string url = "http://localhost/AR/Signup.php" ;
        StartCoroutine(Registration());
    }
    


    IEnumerator Registration()
    {      
            WWWForm form = new WWWForm();
            form.AddField("name", Username.text);
            form.AddField("password", Password.text);
            form.AddField("Email", EmailField.text);
            WWW www = new WWW("http://localhost/AR/Signup.php", form);
            yield return www;
            if (www.text == "0")
            {
                Debug.Log("User creation Failed.Error #" + www.text);
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                info.color=Color.green;
                info.text = "User created successfully.";
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
    }

}
    

    



