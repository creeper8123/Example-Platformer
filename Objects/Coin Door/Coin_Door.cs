using Godot;
using System;

public class Coin_Door : StaticBody2D
{
    [Export]
    bool isOpen = false;

    [Export]
    int coinCost = 10;

    public override void _Ready(){
        GetNode<Label>("Sprite/Label").Text = "" + coinCost;

        if(isOpen){
            openDoor();
        }
    }

    private void openDoor(){
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        GetNode<Area2D>("Area2D").GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Open");
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
    }

    private void _on_Area2D_body_entered(Node body){
        if(body.GetType().Equals(typeof(Player))){
            Player playerNode = (Player) body;
            if(playerNode.getCoinCount() >= coinCost){
                playerNode.removeCoins(coinCost);
                isOpen = true;
                openDoor();
            }
        }
    }
}
