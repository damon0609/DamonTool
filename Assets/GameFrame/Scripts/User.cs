using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string mName;
    public string name
    {
        get
        {
            return mName;
        }
    }

    private string mPassword;
    public string password
    {
        get { return mPassword; }
    }


    private string mEmail;
    public string email
    {
        get { return mEmail; }
    }


    public User(string name, string password, string email)
    {
        this.mName = name;
        this.mPassword = password;
        this.mEmail = email;
    }

    public void Register(string name, string password)
    {

    }


}
