using UnityEngine;

namespace Orchard.GameSpace
{
    public class LevelLoadingData : MonoBehaviour
    {
        public SecureInt NumberLevel { get; set; }
        public SecureInt ExtraMoves { get; set; }
        public int CountAdFreeLevels { get; set; }
    }
}
