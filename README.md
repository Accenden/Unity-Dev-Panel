# Modular Command Console System for Unity

A simple, modular **Command Console system** for Unity written in **C#**, designed to be easily expandable and developer-friendly.  
This was created for debug purposes in big projects, making it much easier to make a new Command simply by adding a new script with logic.  
This project should be compatible with most Unity versions, but only `Unity 6` and above were tested.

Commands are added by attaching new scripts to any `GameObject` that implements the `ICommand` interface.

A visual example of the Console:

<p align="center">
  <img src="https://i.imgur.com/WCNg4eR.png" width="700" alt="Command Console Example" />
</p>

## Features

- ✅ Modular command registration (drop-in commands)
- ✅ Clean separation of **UI** and **command logic**
- ✅ Easily expandable command set (add commands without editing core code)
- ✅ Optional: aliases, help text, argument parsing

#### This is a simple example of GameObjects layout for the Core logic where Dev Console is the Canvas.

<p align="left">
  <img src="https://i.imgur.com/MZ724Q9.png" width="300" alt="Command Console Core Layout" />
</p>

Note that this project is a WIP.
