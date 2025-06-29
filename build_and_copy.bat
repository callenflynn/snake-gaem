@echo off
REM Publish the project as a self-contained, single EXE for Windows
dotnet publish "D:\code\SnakeGame\SnakeGame\SnakeGame.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

REM Copy the latest EXE to the SnakeGame folder (overwrite if exists)
copy /Y "D:\code\SnakeGame\SnakeGame\bin\Release\net6.0-windows\win-x64\publish\SnakeGame.exe" "D:\code\SnakeGame\SnakeGame.exe"

REM Run the EXE in a new window
start "" "D:\code\SnakeGame\SnakeGame.exe"

echo Build, copy, and launch complete.
pause