using System;
using UnityEngine;

public interface ICommand
{
    string Name { get; }
    void Execute(string[] args);

    void Initialize()
    {
        Utils.Console.Instance.addCommand(this);
    }
    void OnDisable();
}
