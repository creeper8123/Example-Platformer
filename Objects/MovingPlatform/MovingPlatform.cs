using Godot;
using System;

public class MovingPlatform : Node2D
{
    [Export]
    private Vector2 startPosition = new Vector2(0, 0);

    [Export]
    private Vector2 endPosition = new Vector2(100, 100);

    [Export]
    private float speedUnitsPerSecond = 128f;

    private Tween tweenNode;
    private float dist;
    private bool movingBackwards = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dist = startPosition.DistanceTo(endPosition);
        tweenNode = GetNode<Tween>("Tween");

        _on_Tween_tween_completed(this, "position");
    }

    private void _on_Tween_tween_completed(Godot.Object obj, NodePath key){
        if(!tweenNode.IsActive() || !movingBackwards){
            tweenNode.Stop(this, "position");
            tweenNode.InterpolateProperty(this, "position", startPosition, endPosition, dist/speedUnitsPerSecond, Tween.TransitionType.Sine, Tween.EaseType.InOut);
            tweenNode.Start();
            movingBackwards = true;
        }else{
            tweenNode.Stop(this, "position");
            tweenNode.InterpolateProperty(this, "position", endPosition, startPosition, dist/speedUnitsPerSecond, Tween.TransitionType.Sine, Tween.EaseType.InOut);
            tweenNode.Start();
            movingBackwards = false;
        }
    }
}
