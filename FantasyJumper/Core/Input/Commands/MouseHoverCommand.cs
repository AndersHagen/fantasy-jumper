namespace FantasyJumper.Core.Input.Commands
{
    public class MouseHoverCommand : GameCommand
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public MouseHoverCommand(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
