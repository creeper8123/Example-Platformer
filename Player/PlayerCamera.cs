using Godot;
using System;

public class PlayerCamera : Camera2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private static Player player;
    private Vector2 screenSize;

    private int xScreen = 0;
    private int yScreen = 0;
    public override void _Ready()
    {
        SmoothingEnabled = true;
        LimitSmoothed = true;
        Current = true;

        player = GetNode<Player>("..");
        screenSize = GetViewport().Size * Zoom;

        if(player.GlobalPosition.x < 0){
            xScreen -= 1;
        }

        LimitLeft = xScreen * (int)screenSize.x;
        LimitRight = (xScreen+1) * (int)screenSize.x;

        if(player.GlobalPosition.y < 0){
            yScreen -= 1;
        }

        LimitTop = yScreen * (int)screenSize.y;
        LimitBottom = (yScreen+1) * (int)screenSize.y;
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta){
        updateCameraPos();
    }

    private void updateCameraPos(){
        screenSize = GetViewport().Size * Zoom;//In case window resizes

        int xScreenTemp = (int)player.Position.x / (int)screenSize.x;
        int yScreenTemp = (int)player.Position.y / (int)screenSize.y;

        if(player.GlobalPosition.x < 0){
            xScreenTemp -= 1;
        }

        if(xScreen != xScreenTemp){
            xScreen = xScreenTemp;
            LimitLeft = xScreen * (int)screenSize.x;
            LimitRight = (xScreen+1) * (int)screenSize.x;
        }

        if(player.GlobalPosition.y < 0){
            yScreenTemp -= 1;
        }

        if(yScreen != yScreenTemp){
            yScreen = yScreenTemp;
            LimitTop = yScreen * (int)screenSize.y;
            LimitBottom = (yScreen+1) * (int)screenSize.y;
        }
    }
}
