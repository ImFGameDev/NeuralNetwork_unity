using UnityEngine;

namespace Seshihiko.Systems.Entity.Config
{
    [CreateAssetMenu(menuName = "RayScannerConfig", fileName = "new RayScannerConfig")]
    public class RayScannerConfig : ScriptableObject
    {
        [SerializeField] private int _distanceVision = 50;
        [SerializeField] private int _viewingAngle = 45;
        [SerializeField] private int _numberOfRay = 5;
        [SerializeField] private LayerMask _layers;
        
        public int DistanceVision => _distanceVision;
        public int ViewingAngle => _viewingAngle;
        public int NumberOfRay => _numberOfRay;

        public LayerMask LayerMask => _layers;
    }
}