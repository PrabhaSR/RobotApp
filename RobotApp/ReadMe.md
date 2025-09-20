
## Project Structure

/RobotApp - Main console application
/RobotAppTest - Unit test project
/commands.txt - Sample file containing robot commands

## Run the Application
```bash
dotnet run --project RobotApp -- commands.txt

## Run the Unit test
```bash
dotnet test RobotAppTest

## Important Notes
Ensure  commands.txt File is Copied to Output
To make sure commands.txt is available at runtime, update your RobotApp.csproj file with the following:

<ItemGroup>
  <None Update="commands.txt">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>

### Sample Scenarios in commands.txt or Unit Tests
Scenario 1

Input:
PLACE 0,0,NORTH
MOVE
REPORT

Output:
0,1,NORTH
Scenario 2

Input:
PLACE 0,0,NORTH
LEFT
REPORT

Output:
0,0,WEST

