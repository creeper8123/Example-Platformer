using Godot;
using System;

public class Player : KinematicBody2D{
    private Vector2 movementVector = Vector2.Zero;
    private const int MAX_H_SPEED = 400;
    private const float H_ACCELERATION = 25f;
    private const float H_FRICTION = 15f;
    private const float GRAVITY = 20f;
    private const int JUMP_FORCE = 825;
    private const int INTERIA = 200;


    private int coins = 0;

    public override void _Ready(){
        updateCoinCount();
    }

    public override void _PhysicsProcess(float delta){

        if(Input.IsActionPressed("move_left") && movementVector.x > -MAX_H_SPEED){
            movementVector.x -= H_ACCELERATION;
        }else if(Input.IsActionPressed("move_right") && movementVector.x < MAX_H_SPEED){
            movementVector.x += H_ACCELERATION;
        }else if(IsOnFloor()){
            if(movementVector.x > H_FRICTION){
                movementVector.x -= H_FRICTION;
            }else if(movementVector.x < -H_FRICTION){
                movementVector.x += H_FRICTION;
            }else{
                movementVector.x = 0;
            }
        }

        if(Input.IsActionPressed("move_up") && IsOnFloor()){
            movementVector.y = -JUMP_FORCE;
            GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
        }

        movementVector = MoveAndSlide(linearVelocity: movementVector + (Vector2.Down * GRAVITY), upDirection: Vector2.Up, infiniteInertia: false);
   
        for (int i = 0; i < GetSlideCount(); i++){
            KinematicCollision2D collision = GetSlideCollision(i);
            if(collision.Collider.GetType().Equals(typeof(RigidBody2D))){
                RigidBody2D pushableBody = (RigidBody2D)collision.Collider;
                if(pushableBody.IsInGroup("Pushable")){
                    pushableBody.ApplyImpulse(collision.Position, -collision.Normal * INTERIA);
                }
            }
        }
    }

    public void addCoins(int numOfCoins){
        coins += numOfCoins;
        if(coins>1000){
            coins=999;
        }
        updateCoinCount();
    }

    public void removeCoins(int numOfCoins){
        coins -= numOfCoins;
        if(coins<0){
            coins=0;
        }
        updateCoinCount();
    }

    public int getCoinCount(){
        return coins;
    }

    public void updateCoinCount(){
        GetNode<Label>("CanvasLayer/ColorRect/Label").Text = "x" + getCoinCount().ToString("D3");

    }

    public void waitForNextLevel(){
        SetPhysicsProcess(false);
        GetNode<Camera2D>("Camera2D").SmoothingEnabled = false;
    }
}