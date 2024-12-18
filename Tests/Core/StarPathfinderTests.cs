using Game.Players.Star;

namespace Tests.Core;

public class StarPathfinderTests
{
    [Test]
    public void Should_get_correct_path_for_start_4_x_4_grid_with_food_on_0_0()
    {
        // Arrange
        int[,] grid =
        {
            {4, 0, 0, 0},
            {0, 0, 0, 0},
            {3, 1, 0, 0},
            {0, 0, 0, 0},
        };

        // Act
        var path = StarPathfinder.FindPath(grid, new(2, 1), new(2, 0), new(0, 0));

        // Assert
        path.Should().Be("UUL");
    }

    [Test]
    public void Should_get_correct_path_for_start_4_x_4_grid_with_food_on_0_3()
    {
        // Arrange
        int[,] grid =
        {
            {0, 0, 0, 4},
            {0, 0, 0, 0},
            {3, 1, 0, 0},
            {0, 0, 0, 0},
        };

        // Act
        var path = StarPathfinder.FindPath(grid, new(2, 1), new(2, 0), new(0, 3));

        // Assert
        path.Should().Be("URRU");
    }

    [Test]
    public void Should_get_correct_path_for_start_4_x_4_grid_with_food_on_3_0()
    {
        // Arrange
        int[,] grid =
        {
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {3, 1, 0, 0},
            {4, 0, 0, 0},
        };

        // Act
        var path = StarPathfinder.FindPath(grid, new(2, 1), new(2, 0), new(3, 0));

        // Assert
        path.Should().Be("L");
    }

    [Test]
    public void Should_get_correct_path_for_start_4_x_4_grid_with_food_on_3_3()
    {
        // Arrange
        int[,] grid =
        {
            {0, 0, 0, 0},
            {0, 0, 0, 0},
            {3, 1, 0, 0},
            {0, 0, 0, 4},
        };

        // Act
        var path = StarPathfinder.FindPath(grid, new(2, 1), new(2, 0), new(3, 3));

        // Assert
        path.Should().Be("URDDR");
    }

    [Test]
    public void Should_get_correct_path_for_start_4_x_4_grid_with_food_on_3_3_and_body_block_food_path()
    {
        // Arrange
        int[,] grid =
        {
            {0, 0, 0, 0},
            {0, 2, 2, 1},
            {3, 2, 0, 0},
            {0, 0, 0, 4},
        };

        // Act
        var path = StarPathfinder.FindPath(grid, new(1, 3), new(2, 0), new(3, 3));

        // Assert
        path.Should().Be("ULLLDD");
    }
}
