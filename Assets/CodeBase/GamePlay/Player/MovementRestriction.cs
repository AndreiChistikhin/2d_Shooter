using System;
using UnityEngine;

public class MovementRestriction
{
    private readonly float _minX;
    private readonly float _maxX;
    private readonly float _minY;
    private readonly float _maxY;
    
    public MovementRestriction(Camera camera, float yMaxRestriction)
    {
        _minX = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        _maxX = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        _minY = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        _maxY = yMaxRestriction;
    }

    public Vector2 ClampPosition(Vector2 position)
    {
        float xPosition = Math.Clamp(position.x, _minX, _maxX);
        float yPosition = Math.Clamp(position.y, _minY, _maxY);

        return new Vector2(xPosition, yPosition);
    }
}