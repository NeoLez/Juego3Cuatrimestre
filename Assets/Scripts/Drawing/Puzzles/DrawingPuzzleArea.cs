using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawingPuzzleArea : MonoBehaviour {
    [SerializeField] private float xSize;
    [FormerlySerializedAs("ySize")] [SerializeField] private float zSize;

    [SerializeField] private Transform[] objectsPos;

    [SerializeField] private DrawingSurfacePuzzle surface;

    [SerializeField] private Vector3 rotation;

    private void Update() {
        transform.rotation *= Quaternion.Euler(rotation* Time.deltaTime);
        for (int i = 0; i < objectsPos.Length; i++) {
            float3x3 matrix = new float3x3(new float3(transform.forward), new float3(transform.up), new float3(transform.right));
            float3 localPos = math.mul(new float3(objectsPos[i].position - transform.position), matrix) / new float3(xSize, 1, zSize);
            surface.points[i].position = new Vector2(localPos.x, localPos.z);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        foreach (var objPos in objectsPos) {
            Gizmos.DrawSphere(Vector3.ProjectOnPlane(objPos.transform.position - transform.position, transform.up) + transform.position, 0.05f);
        }
    }
}