using UnityEngine;

public interface IProjectile
{
    void SetStartingPoint(Transform _point);
    void SetDirection(Vector3 _dir);
}
