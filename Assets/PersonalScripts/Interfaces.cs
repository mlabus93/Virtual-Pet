using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeDamage(int amount, Vector3 hitPoint);
    void Death();
}

public interface IGivePoints
{
    void GivePoints(int amount);
}

public interface IInteractable
{
    void Interact();
}