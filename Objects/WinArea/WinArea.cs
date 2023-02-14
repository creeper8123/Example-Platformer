using Godot;
using System;

public class WinArea : Area2D
{
    [Export]
    float levelEndDelaySec = 2f;

    [Export]
    int nextLevel = 1;

    public override void _Ready()
    {
        GetNode<Timer>("Timer").WaitTime = levelEndDelaySec;
    }

    private void _on_WinArea_body_entered(Node body){
        if(body.GetType().Equals(typeof(Player))){
            Player playerNode = (Player) body;
            playerNode.waitForNextLevel();
            GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            GetNode<Timer>("Timer").Start();
        }
    }

    private void _on_Timer_timeout(){
        GetTree().ChangeScene("res://Levels/Level" + nextLevel + ".tscn");
    }
}
