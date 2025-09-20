using RobotApp.Interfaces;
using RobotApp.Model;
using RobotApp.Services;

namespace RobotAppTest
{
    public class RobotAppTest
    {
        [Fact]
        public void PlaceMoveReport_WorksCorrectly_Example1()
        {
            // Arrange
            ISurface table = new Table();  // The surface or table the robot moves on
            IRobot robot = new Robot(table);

            // Act
            robot.Place(0, 0, Direction.NORTH);
            robot.Move();
            var report = robot.Report();

            // Assert
            Assert.Equal("0,1,NORTH", report);
        }

        [Fact]
        public void PlaceMoveReport_WorksCorrectly_Example2()
        {
            // Arrange
            ISurface table = new Table();  // The surface or table the robot moves on
            IRobot robot = new Robot(table);

            // Act
            robot.Place(0, 0, Direction.NORTH);
            robot.Left();
            var report = robot.Report();

            // Assert
            Assert.Equal("0,0,WEST", report);
        }

        [Theory(DisplayName = "Robot does not move beyond the table edge")]
            [InlineData(0, 4, Direction.NORTH)] // Top edge
            [InlineData(0, 0, Direction.SOUTH)] // Bottom edge
            [InlineData(4, 0, Direction.EAST)]  // Right edge
            [InlineData(0, 0, Direction.WEST)]  // Left edge
            public void Move_AtTableEdge_DoesNotChangePosition(int startX, int startY, Direction direction)
            {
                // Arrange
                ISurface table = new Table();
                IRobot robot = new Robot(table);
                robot.Place(startX, startY, direction);

                // Act
                robot.Move();
                var report = robot.Report();

                // Assert
                Assert.Equal(startX, robot.Position.X);
                Assert.Equal(startY, robot.Position.Y);
            }

            [Theory(DisplayName = "Robot ignores invalid PLACE coordinates")]
            [InlineData(-1, 0)] // X < 0
            [InlineData(0, -1)] // Y < 0
            [InlineData(5, 0)]  // X > max
            [InlineData(0, 5)]  // Y > max
            public void Place_InvalidCoordinates_DoesNotPlaceRobot(int x, int y)
            {
                // Arrange
                ISurface table = new Table();
                IRobot robot = new Robot(table);

                // Act
                robot.Place(x, y, Direction.NORTH);

                // Assert
                Assert.False(robot.IsPlaced);
            }

            [Fact]
            public void Robot_IgnoresCommandsBeforeValidPlace()
            {
                // Arrange
                ISurface table = new Table();
                IRobot robot = new Robot(table);

                // Act
                robot.Move();
                robot.Left();
                robot.Right();
                var report = robot.Report();

                // Assert
                Assert.False(robot.IsPlaced);
                Assert.Equal("Robot not yet placed.", report); //Robot not yet placed.
            }

            [Fact]
            public void Robot_CorrectlyPlacesAtValidPosition()
            {
                ISurface table = new Table();
                IRobot robot = new Robot(table);

                robot.Place(0, 0, Direction.NORTH);

                Assert.True(robot.IsPlaced);
                Assert.Equal(0, robot.Position.X);
                Assert.Equal(0, robot.Position.Y);
                Assert.Equal(Direction.NORTH, robot.Position.Facing);
            }

            [Theory(DisplayName = "Verify Robot turn left correctly")]
            [InlineData(Direction.NORTH, Direction.WEST)]
            [InlineData(Direction.WEST, Direction.SOUTH)]
            [InlineData(Direction.SOUTH, Direction.EAST)]
            [InlineData(Direction.EAST, Direction.NORTH)]
            public void Left_FromDirection_RotatesCorrectly(Direction start, Direction expected)
            {
                // Arrange
                ISurface table = new Table();
                IRobot robot = new Robot(table);
                robot.Place(2, 2, start);

                // Act
                robot.Left();

                // Assert
                Assert.Equal(expected, robot.Position.Facing);
            }

            [Theory(DisplayName = "Verify Robot turn right correctly")]
            [InlineData(Direction.NORTH, Direction.EAST)]
            [InlineData(Direction.EAST, Direction.SOUTH)]
            [InlineData(Direction.SOUTH, Direction.WEST)]
            [InlineData(Direction.WEST, Direction.NORTH)]
            public void Right_FromDirection_RotatesCorrectly(Direction start, Direction expected)
            {
                // Arrange
                ISurface table = new Table();
                IRobot robot = new Robot(table);
                robot.Place(2, 2, start);

                // Act
                robot.Right();

                // Assert
                Assert.Equal(expected, robot.Position.Facing);
            }
        }
    }
