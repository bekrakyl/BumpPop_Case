using UnityEngine;

public class Interfaces
{
}


public interface ICollisionBall
{
    Ball OnInteractEnter(Ball ball);
}

public interface ICollisionFinish
{
    void OnInteractFinish(Rigidbody body);
}

public interface ICollisionChain
{
    void OnCollisionChain();
}
