using Godot;
using System;

public class CoinBig : Area2D{
    private void _on_CoinBig_body_entered(Node body){
        if(body.GetType().Equals(typeof(Player))){
            GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
            GetNode<Sprite>("Sprite").Hide();
            GetNode<Particles2D>("Particles2D").Emitting = true;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();

            Player playerNode = (Player) body;
            playerNode.addCoins(10);
        }
    }
}
