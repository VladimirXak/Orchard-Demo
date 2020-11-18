using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class ParticleDestroyGeneralPiece : PoolParticle
    {
        [SerializeField] private List<DataTypeBoardObjectMaterial> _listMaterials;

        private Renderer _renderer;

        protected override void Awake()
        {
            base.Awake();

            _renderer = GetComponent<Renderer>();
        }

        public override void InitMaterial(TypeBoardObject type)
        {
            _renderer.material = _listMaterials.Find(v => v.type == type).material;
        }

        public override IBoardObjectChecking GetTypeChecking()
        {
            return new PieceGeneralChecking();
        }

        [System.Serializable]
        private struct DataTypeBoardObjectMaterial
        {
            public TypeBoardObject type;
            public Material material;
        }
    }
}
