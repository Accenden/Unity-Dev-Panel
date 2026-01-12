using System;
using System.Collections.Generic;
using UnityEngine;

public class Command_test : MonoBehaviour , ICommand
{

    public string Name => "test";
    private List<string> txt = new List<string> { "test" , "test autocomplete"}; 

    public void Execute(string[] args)
    {
        Utils.Console.Instance.toLog("Test command succesfully executed");
    }
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Initialize();
        autoComplete();
    }

    void Initialize() 
    {

        Utils.Console.Instance.addCommand(this);
    }

    public void OnDisable()
    {
        if(Utils.Console.Instance != null)
        {
            Utils.Console.Instance.removeCommand(this);
            Utils.Console.Instance.rmvAutoComplete(txt);
        }


    }
    public void OnDestroy()
    {
        if(Utils.Console.Instance != null)
        {
            Utils.Console.Instance.removeCommand(this);
            Utils.Console.Instance.rmvAutoComplete(txt);
        }

    }

    private void autoComplete()
    {
        
        Utils.Console.Instance.addAutoComplete(txt);
        //basic implementation for now
    }
}
