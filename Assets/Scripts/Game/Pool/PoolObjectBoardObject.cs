using UnityEngine;

namespace Orchard.Game
{
    public class PoolObjectBoardObject : MonoBehaviour
    {
        private PoolBoardObject _poolBoardObject;

        public void SetObjectPooling(PoolBoardObject dataPoolGroup)
        {
            _poolBoardObject = dataPoolGroup;
        }

        public void ReturnUnitToPool()
        {
            _poolBoardObject.ReturnUnitToPool(gameObject);
        }
    }
}
