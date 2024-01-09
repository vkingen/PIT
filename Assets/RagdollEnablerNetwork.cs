using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RagdollEnablerNetwork : NetworkBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private Transform RagdollRoot;
    [SerializeField]
    private bool StartRagdoll = false;
    // Only public for Ragdoll Runtime GUI for explosive force
    public Rigidbody[] Rigidbodies;
    private CharacterJoint[] Joints;
    private Collider[] Colliders;

    private void Awake()
    {
        Rigidbodies = RagdollRoot.GetComponentsInChildren<Rigidbody>();
        Joints = RagdollRoot.GetComponentsInChildren<CharacterJoint>();
        Colliders = RagdollRoot.GetComponentsInChildren<Collider>();

        if (StartRagdoll)
        {
            EnableRagdoll();
        }
        else
        {
            EnableAnimator();
        }
    }

    public void EnableRagdoll()
    {
        EnableRagdollServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void EnableRagdollServerRpc()
    {
        Animator.enabled = false;
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = true;
        }
        foreach (Collider collider in Colliders)
        {
            collider.enabled = true;
        }
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            //rigidbody.AddForce(transform.up * 500);
        }
    }

    public void EnableAnimator()
    {
        Animator.enabled = true;
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = false;
        }
        foreach (Collider collider in Colliders)
        {
            collider.enabled = false;
        }
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
        }
    }
}
