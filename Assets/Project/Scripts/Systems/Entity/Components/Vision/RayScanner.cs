using Seshihiko.Systems.Entity.Components.Rays;
using Seshihiko.Systems.Entity.Config;
using Seshihiko.Systems.Entity.Objects;
using UnityEngine;

namespace Seshihiko.Systems.Entity.Components
{
    public class RayScanner
    {
        private VisionResult _result;
        private RayScannerConfig _config;

        public RayScanner(RayScannerConfig config)
        {
            _config = config;
            
            _result.RaysData = new RayData[_config.NumberOfRay];
        }

        private void DrawRay(Transform positionRay , Vector3 direction, float distance)
        {
            Debug.DrawRay(positionRay.position, direction * distance, Color.blue);
        }
        
        public VisionResult Scan(Transform positionRay)
        {
            var angle = (float)_config.ViewingAngle / 2;
            var angleIteration = (float)_config.ViewingAngle / _config.NumberOfRay;
            var angleRay = 0f;

            for (byte i = 0; i < _config.NumberOfRay; i++)
            {
                var newVector = Quaternion.AngleAxis(-angle + angleRay, Vector3.up) * positionRay.forward;
                var Ray = new Ray(positionRay.position, newVector);

                if (Physics.Raycast(Ray, out var hit, _config.DistanceVision, _config.LayerMask))
                {
                    if (hit.collider.TryGetComponent<IEnvironmentObject>(out var environment))
                    {
                        var distance = Vector3.Distance(positionRay.position,
                            hit.point);
                        
                        _result.RaysData[i].Distance = distance / _config.DistanceVision;
                        _result.RaysData[i].EnvironmentObject = environment.GetData();
                        
                        DrawRay(positionRay, newVector, distance);
                    }   
                }
                else
                {
                    _result.RaysData[i].Distance = 1;
                    _result.RaysData[i].EnvironmentObject.TypeObject = 0;
                    _result.RaysData[i].EnvironmentObject.LocalIndex = 0;
                }
                
                angleRay += angleIteration;
            }
            
            return _result;
        }
    }
}