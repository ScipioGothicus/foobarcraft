using Microsoft.Xna.Framework;

namespace MinicraftClone;

public class Camera(int width, int height, Vector2 position)
{

    public Vector2 Position { get; set; } = position;
    public Matrix TransformMatrix = Matrix.CreateTranslation(position.X, position.Y, 0);
    private const float FollowSpeed = 16.0f;
    private Vector2 _velocity;
    
    public void Update(Vector2 targetPosition, GameTime gameTime)
    {
        // thx to winkio2 from reddit for an implementation of simple camera physics
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 aSpring = (FollowSpeed * FollowSpeed / 4) * (targetPosition - Position);
        Vector2 aDamping = -1 * FollowSpeed * _velocity;
        Vector2 cameraAcceleration = aSpring + aDamping;
        _velocity += cameraAcceleration * dt;
        if (Vector2.DistanceSquared(_velocity, Vector2.Zero) < 1 && Vector2.DistanceSquared(cameraAcceleration, Vector2.Zero) < 1)
            _velocity = Vector2.Zero;
        Position += _velocity * dt;
        TransformMatrix = Matrix.CreateTranslation(width / 2.0f - Position.X, height / 2.0f - Position.Y, 0);
    }
}