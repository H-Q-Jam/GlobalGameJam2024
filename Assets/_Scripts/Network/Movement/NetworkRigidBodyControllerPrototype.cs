using System;
using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
// ReSharper disable once CheckNamespace
public class NetworkRigidbodyControllerPrototype : NetworkRigidbody
{
  [Header("Rigidbody Controller Settings")]
  public float gravity       = -9.81f;
  public float jumpImpulse   = 8.0f;
  public float acceleration  = 10.0f;
  public float braking       = 10.0f;
  public float maxSpeed      = 2.0f;
    public float speed = 3.0f;
  public float rotationSpeed = 15.0f;

    public Rigidbody rb;
  [Networked]
  [HideInInspector]
  public bool IsGrounded { get; set; }

  [Networked]
  [HideInInspector]
  public Vector3 Velocity { get; set; }

  /// <summary>
  /// Sets the default teleport interpolation velocity to be the CC's current velocity.
  /// For more details on how this field is used, see <see cref="NetworkTransform.TeleportToPosition"/>.
  /// </summary>
  protected override Vector3 DefaultTeleportInterpolationVelocity => Velocity;

  /// <summary>
  /// Sets the default teleport interpolation angular velocity to be the CC's rotation speed on the Z axis.
  /// For more details on how this field is used, see <see cref="NetworkTransform.TeleportToRotation"/>.
  /// </summary>
  protected override Vector3 DefaultTeleportInterpolationAngularVelocity => new Vector3(0f, 0f, rotationSpeed);

  public Rigidbody Controller { get; private set; }

  protected override void Awake() {
    base.Awake();
    CacheController();
  }

  public override void Spawned() {
    base.Spawned();
        rb = GetComponent<Rigidbody>();
    CacheController();
  }

  private void CacheController() {
    if (Controller == null) {
      Controller = GetComponent<Rigidbody>();

      Assert.Check(Controller != null, $"An object with {nameof(NetworkRigidbodyControllerPrototype)} must also have a {nameof(Rigidbody)} component.");
    }
  }

  protected override void CopyFromBufferToEngine() {

  }

  /// <summary>
  /// Basic implementation of a jump impulse (immediately integrates a vertical component to Velocity).
  /// <param name="ignoreGrounded">Jump even if not in a grounded state.</param>
  /// <param name="overrideImpulse">Optional field to override the jump impulse. If null, <see cref="jumpImpulse"/> is used.</param>
  /// </summary>
  public virtual void Jump(bool ignoreGrounded = false, float? overrideImpulse = null) {
    if (IsGrounded || ignoreGrounded) {
      var newVel = Velocity;
      newVel.y += overrideImpulse ?? jumpImpulse;
      Velocity =  newVel;
    }
  }

  /// <summary>
  /// Basic implementation of a character controller's movement function based on an intended direction.
  /// <param name="direction">Intended movement direction, subject to movement query, acceleration and max speed values.</param>
  /// </summary>
  public virtual void Move(Vector3 direction) 
  {
        Debug.Log(IsGrounded);
        Debug.Log("Moving to : " + direction);
        //is grounded todo
    if (IsGrounded) 
    {
            
    }
        rb.velocity = direction * speed;
        Debug.Log("Rigidbody Velocity : " + rb.velocity);
    }
}