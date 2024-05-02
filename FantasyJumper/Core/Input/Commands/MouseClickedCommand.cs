namespace FantasyJumper.Core.Input.Commands
{
    public class MouseClickedCommand : GameCommand
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MouseClickedCommand(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
