using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    [CreateAssetMenu(fileName = "DataBoardObject", menuName = "ScriptableObject/DataBoardObject")]
    public class DataBoardObject : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClipDestroy;
        [SerializeField] private List<DataSpriteBoardObject> listDataSpriteBoardObjects;

        public AudioClip AudioClipDestroy => _audioClipDestroy;

        public Sprite GetSprite(TypeBoardObject type)
        {
            return listDataSpriteBoardObjects.Find(item => item.TypeBoardObject == type).Sprite;
        }

        public IEnumerator<TypeBoardObject> GetEnumerator()
        {
            foreach (var dataSprite in listDataSpriteBoardObjects)
                yield return dataSprite.TypeBoardObject;
        }
    }
}
