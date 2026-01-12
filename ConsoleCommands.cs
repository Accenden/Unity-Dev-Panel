using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Utils
{

    public class ConsoleCommands
    {
        private readonly Dictionary<string, ICommand> commands;

        public ConsoleCommands(Dictionary<string, ICommand> commands)
        {
            this.commands = commands;
        }

        public void CommandManager(string commandInput)
        {
            if (string.IsNullOrWhiteSpace(commandInput))
            {
                Debug.LogWarning("Empty command input.");
                return;
            }

            string[] parts = commandInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return;
            }

            var key = parts[0].ToLowerInvariant();
            if (commands.TryGetValue(key, out var command))
            {
                // All tokens except the first (key) are passed as args
                var args = parts.Skip(1).ToArray();
                command.Execute(args);
            }
            else
            {
                Debug.LogWarning($"Unknown command: {key}"); //Can be changed later to be in custom console UI
            }
        }
    }
}
