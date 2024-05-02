namespace FantasyJumper.Core.Input.Commands
{
    public class MainMenuCommand : GameCommand
    {
        public bool IsPaused { get; private set; }

        public MainMenuCommand(bool pause = false) { IsPaused = pause; }
    }
}
