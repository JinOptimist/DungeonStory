namespace Assets.Maze.Cell
{
    public interface ICell
    {
        int X { get; set; }
        int Z { get; set; }

        IMazeLevelBusinessObject Maze { get; set; }
    }
}