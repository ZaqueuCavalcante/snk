namespace Game.Core;

public class GameState
{
    public int Rows { get; }
    public int Columns { get; }
    public Grid Grid { get; }
    public Snake Snake { get; }

    public bool IsRunning { get; set; }
    public int Score { get; private set; }
    public int Steps { get; private set; }
    public bool GameOver { get; private set; }
    public bool Zerou { get; private set; }

	public GameState(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Grid = new Grid(rows, columns);
        Snake = new Snake();

        AddSnake();
        Grid.AddFood();
    }

    private void AddSnake()
    {
        int middleRow = Rows / 2;
        for (int column = 2; column >= 1; column--)
        {
            Grid.PutSnake(middleRow, column);
            Snake.Append(middleRow, column);
        }
    }

    private void MoveSnakeHead(Position position)
    {
        Grid.PutSnake(position);
        Snake.MoveTo(position);
    }

    private void RemoveSnakeTail()
    {
        Grid.PutEmpty(Snake.GetTailPosition());
        Snake.DropTail();
    }

    public void MoveSnake()
    {
        Snake.ChangeDirection();
        Steps++;

        var newHeadPosition = Snake.NextHeadPosition();
        var targetCell = WillHit(newHeadPosition);

        if (targetCell is CellType.Outside or CellType.SnakeBody)
        {
            GameOver = true;
            return;
        }

        if (targetCell == CellType.Empty)
        {
            RemoveSnakeTail();
            MoveSnakeHead(newHeadPosition);
            return;
        }

        if (targetCell == CellType.Food)
        {
            MoveSnakeHead(newHeadPosition);
            Score++;
            Zerou = Grid.AddFood() == 0;
        }
    }

    private CellType WillHit(Position newHeadPosition)
    {
        if (Grid.IsOutside(newHeadPosition)) return CellType.Outside;

        if (newHeadPosition == Snake.GetTailPosition()) return CellType.Empty;

        return Grid.GetCellAt(newHeadPosition);
    }

    public GameStateData GetData()
    {
        var data = new GameStateData
        {
            Rows = Rows,
            Columns = Columns,
            SnakeSize = Snake.Size(),
            HeadPosition = Snake.GetHeadPosition(),
            HeadDirection = Snake.GetHeadDirection(),
            TailPosition = Snake.GetTailPosition(),
            FoodPosition = Grid.GetFoodPosition(),
        };

        data.Grid = new int[Rows, Columns];
		for (int row = 0; row < Rows; row++)
		{
			for (int column = 0; column < Columns; column++)
			{
                data.Grid[row, column] = (int) Grid.GetCellAt(row, column);
			}
		}
        data.Grid[data.HeadPosition.Row, data.HeadPosition.Column] = (int) CellType.SnakeHead;
        data.Grid[data.TailPosition.Row, data.TailPosition.Column] = (int) CellType.SnakeTail;

        data.HeadRight = WillHit(data.HeadPosition.MoveTo(Direction.Right));
        data.HeadDown = WillHit(data.HeadPosition.MoveTo(Direction.Down));
        data.HeadLeft = WillHit(data.HeadPosition.MoveTo(Direction.Left));
        data.HeadUp = WillHit(data.HeadPosition.MoveTo(Direction.Up));

        return data;
    }

	public void SnakeGoTo(Direction direction)
    {
        Snake.GoTo(direction);
    }
}
