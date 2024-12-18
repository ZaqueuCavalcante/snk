namespace Game.Core;

public class Position
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        Row = row;
		Column = column;
    }

	public Position MoveTo(Direction direction)
	{
		return new(Row + direction.Y, Column + direction.X);
	}

	public bool IsInside(int rows, int columns)
	{
		return Row >= 0 && Row < rows && Column >= 0 && Column < columns;
	}

	public override bool Equals(object? obj)
	{
		return obj is Position position &&
			Row == position.Row &&
			Column == position.Column;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Row, Column);
	}

	public static bool operator ==(Position? left, Position? right)
	{
		return EqualityComparer<Position>.Default.Equals(left, right);
	}

	public static bool operator !=(Position? left, Position? right)
	{
		return !(left == right);
	}
}
