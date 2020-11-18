using Orchard.GameSpace.Advertisements;
using UnityEngine;

namespace Orchard.GameSpace
{
    public class GameContainer : Singleton<GameContainer>
    {
        protected override void Awake()
        {
            base.Awake();

            GameInfo.Init();
            Localization.Init();
            AdsController.Init();
        }

        [SerializeField] private GameInfo _gameInfo;
        public GameInfo GameInfo => _gameInfo;

        [SerializeField] private GameConfig _gameConfig;
        public GameConfig GameConfig => _gameConfig;

        [SerializeField] private LevelLoadingData _levelLoadingData;
        public LevelLoadingData LevelLoadingData => _levelLoadingData;

        [SerializeField]
        private Audio _audio;
        public Audio Audio => _audio;

        [SerializeField] private AdsController _adsController;
        public AdsController AdsController => _adsController;

        [SerializeField] private Localization _localization;
        public Localization Localization => _localization;
    }
}
