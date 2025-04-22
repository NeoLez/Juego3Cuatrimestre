using UnityEngine;

[CreateAssetMenu(fileName = "DashCardInfo", menuName = "SO/CardInfo/DashCardInfo")]
public class DashCardInfoCameraSO : CardInfoSO {
    [SerializeField] public float moveDistance;
    [SerializeField] public float time;
    [SerializeField] public AnimationCurve curve; 
    
    public override Card GetCard(int position) {
        return new DashCard(this, position);
    }
}