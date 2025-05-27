using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrateWall : MonoBehaviour {
    [SerializeField] public List<GameObject> crateList;
    [SerializeField] public List<int> crateListIDs;
    private Collider selfCollider;
    private int wallInstanceId;
    

    private void Awake() {
        selfCollider = GetComponent<Collider>();
        selfCollider.hasModifiableContacts = true;

        wallInstanceId = GetComponent<Rigidbody>().GetInstanceID();

        foreach (var crate in crateList) {
            crate.GetComponent<Collider>().hasModifiableContacts = true;
            crateListIDs.Add(crate.GetComponent<Rigidbody>().GetInstanceID());
        }
    }
    
    void OnEnable() => Physics.ContactModifyEvent += OnContactModify;
    void OnDisable() => Physics.ContactModifyEvent -= OnContactModify;

    void OnContactModify(PhysicsScene scene, NativeArray<ModifiableContactPair> pairs)
    {
        foreach (ModifiableContactPair pair in pairs) {
            if (pair.bodyInstanceID == wallInstanceId || pair.otherBodyInstanceID == wallInstanceId) {
                foreach (var crateID in crateListIDs) {
                    if (pair.bodyInstanceID == crateID || pair.otherBodyInstanceID == crateID) {
                        for (int i = 0; i < pair.contactCount; i++) {
                            pair.IgnoreContact(i);
                        }
                        break;
                    }
                }
            }
        }
    }
}