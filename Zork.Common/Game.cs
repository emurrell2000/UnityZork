using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Zork
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public static Game Instance { get; private set; }

        public World World { get; private set; }

        public string StartingLocation { get; set; }
        
        public string WelcomeMessage { get; set; }
        
        public string ExitMessage { get; set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        public bool IsRunning { get; set; }

        [JsonIgnore]
        public IInputService Input { get; private set; }

        [JsonIgnore]
        public IOutputService Output { get; private set; }

        [JsonIgnore]
        public Dictionary<string, Command> Commands { get; private set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;

            Commands = new Dictionary<string, Command>()
            {
                { "QUIT", new Command("QUIT", new string[] { "QUIT", "Q", "BYE" }, Quit) },
                { "LOOK", new Command("LOOK", new string[] { "LOOK", "L" }, Look) },
                { "NORTH", new Command("NORTH", new string[] { "NORTH", "N" }, game => Move(game, Directions.North)) },
                { "SOUTH", new Command("SOUTH", new string[] { "SOUTH", "S" }, game => Move(game, Directions.South)) },
                { "EAST", new Command("EAST", new string[] { "EAST", "E"}, game => Move(game, Directions.East)) },
                { "WEST", new Command("WEST", new string[] { "WEST", "W" }, game => Move(game, Directions.West)) },
                { "SCORE", new Command("SCORE", new string[] { "SCORE", "SC" }, Score) },
                { "REWARD", new Command("REWARD", new string[] { "REWARD", "R" }, Reward },
            };
        }

        private void Score(Game game)
        {
            Output.WriteLine($"Your score is {Player.Score} in {Player.Moves} moves.");
        }
        private void Reward(Game game)
        {
            Player.Score++;
        }

        public static void StartFromFile(string gameFilename, IInputService input, IOutputService output)
        {
            if (!File.Exists(gameFilename))
            {
                throw new FileNotFoundException("Expected file.", gameFilename);
            }

            Start(File.ReadAllText(gameFilename), input, output);
        }

        public static void Start(string gameJsonString, IInputService input, IOutputService output)
        {
            Instance = Load(gameJsonString);
            Instance.Input = input;
            Instance.Output = output;
            Instance.DisplayWelcomeMessage();
            Instance.IsRunning = true;
            Instance.Input.InputRecieved += Instance.InputRecievedHandler;
        }

        private void InputRecievedHandler(object sender, string inputString)
        {
            Command foundCommand = null;
            foreach (Command command in Commands.Values)
            {
                if (command.Verbs.Contains(inputString))
                {
                    foundCommand = command;
                    break;
                }
            }
            
            if (foundCommand != null)
            {
                foundCommand.Action(this);
                Player.Moves++;
            }
            else
            {
                Output.WriteLine("Unknown command.");
            }
        }

        public static Game Load(string jsonString)
        {
            Game game = JsonConvert.DeserializeObject<Game>(jsonString);

            return game;
        }

        private static void Move(Game game, Directions direction)
        {
            if (game.Player.Move(direction) == false)
            {
                game.Output.WriteLine("The way is shut!");
            }
        }

        public static void Look(Game game) => game.Output.WriteLine($"{game.Player.Location}\n {game.Player.Location.Description}");

        private static void Quit(Game game)
        {
            game.IsRunning = false;

        }

        private void DisplayWelcomeMessage() => Output.WriteLine(WelcomeMessage);

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Player = new Player(World, StartingLocation);
            Player.Moves = 0;
            Player.Score = 0;
        }
    }
}