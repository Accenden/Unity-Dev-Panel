using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

namespace Utils
{
    public class Console : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        [SerializeField] TMP_Text logs;
        [SerializeField] TMP_Text textComplete;
        public static Console Instance { get; private set; }
        private bool _requestOpen = false;
        private ConsoleCommands consoleCommands;

        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        private List<string> autoCompleteHeads = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            //Ensure this is the only instance.

            Instance = this;
            consoleCommands = new ConsoleCommands(commands);
            DontDestroyOnLoad(gameObject);
        }

        void Start() //ensure everything is set up first
        {
            inputField.onSubmit.AddListener(OnSubmit);
            inputField.onValueChanged.AddListener(autoComplete);

        }
        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
 

        public void toLog(String value)
        {
            logs.text += "\n" + value;
        }

        public void OpenConsole()
        {
            Debug.Log("open Console");
        }

        public void addCommand(ICommand command)
        {
            if (command == null)
            {
                Debug.LogWarning("Attempted to add a null command to Console.");
                return;
            }

            if (string.IsNullOrWhiteSpace(command.Name))
            {
                Debug.LogWarning("Attempted to add a command without a name or include space.");
                return;
            }

            var key = command.Name.ToLowerInvariant();
            if (commands.TryGetValue(key, out var existing) && existing == command)
            {
                return;
            }

            commands[key] = command;
            Debug.Log($"Command '{command.Name}' added successfully.");
        }

        public void removeCommand(ICommand command)
        {
            if (command == null)
            {
                Debug.LogWarning("Attempted to remove a null command to Console.");
                return;
            }
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                Debug.LogWarning("Attempted to remove a command without a name or include space.");
                return;
            }
            var key = command.Name.ToLowerInvariant();
            if (commands.ContainsKey(key))
            {
                commands.Remove(key);
                Debug.Log($"Command '{command.Name}' removed successfully.");
            }

        }

        public void addAutoComplete(List<string> args)
        {
            if (Instance == null)
            {
                return;
            }
            foreach (string i in args)
            {
                autoCompleteHeads.Add(i);
            }
        }

        public void rmvAutoComplete(List<string> args)
        {
            if(args == null)
            {
                return;
            }
            var remove = new HashSet<string>(args);   // O(m)

            for (int i = autoCompleteHeads.Count - 1; i >= 0; i--)
            {
                string x = autoCompleteHeads[i];

                if (remove.Contains(x))   // evaluated PER ELEMENT
                {
                    autoCompleteHeads.RemoveAt(i);
                }
            }
            // O(n);


            //Big O of n

        }

        public void commandPhaser(String input)
        {
            input.Trim();
            input = input.ToLowerInvariant();
            Debug.Log(input);
            String[] args = input.Split(' ');

            if (args.Length == 0)
            {
                Debug.Log("Cmd entered has no input");
            }
            ICommand value;
            if (commands.TryGetValue(args[0], out value))
            {
                args = args.Skip(1).ToArray();
                value.Execute(args);
            }
            else
            {
                Debug.Log("No such command");
                logs.text += "\nNo such command";

            }

        }

        //Listeners
        private void OnSubmit(string value)
        {
            Debug.Log("User typed: " + value);

            if (value.Length != 0)
            {
                commandPhaser(value);
                inputField.text = "";
                inputField.ActivateInputField();
            }



        }
        private void autoComplete(String value)
        {
            //To do add auto complete

            autoCompleteManager(value);

        }

        //Helpers
        private void autoCompleteManager(String value)
        {
            if (value.Length == 0)
            {
                textComplete.text = "";
                return;
            }
            value = value.ToLowerInvariant();

            foreach (string i in autoCompleteHeads)
            {
                if (i.StartsWith(value) && (i.Length > value.Length))
                {
                    int typedLength = value.Length;

                    //number of spaces 
                    string padding = new string(' ', typedLength);

                    //missing part
                    string completion = i.Substring(typedLength);

                    textComplete.text = padding + completion;
                    return;
                }
            }
            textComplete.text = "";
        }




    }
}
