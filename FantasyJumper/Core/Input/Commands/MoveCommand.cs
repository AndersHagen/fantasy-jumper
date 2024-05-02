namespace FantasyJumper.Core.Input.Commands
{
    public class MoveCommand : PlayerCommand
    {
        public int DeltaX { get; private set; }

        public bool Run { get; private set; }

        public MoveCommand(int deltaX, bool run = false)
        {
            DeltaX = deltaX;
            Run = run;
        }
    }
}
