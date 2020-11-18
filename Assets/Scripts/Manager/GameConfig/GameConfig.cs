using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Tooltip("Размер одного тайла")]
    public float TILE_SIZE = 1.1f;
    [Tooltip("Время за которое две фишки меняются местами")]
    public float TIME_SWAP = 0.17f;
    [Tooltip("Расстояние смещения пальца необходимое для запуска движения фишки")]
    public float DELTA_DRAG = 35f;
    [Tooltip("Время уничтожения фишки")]
    public float TIME_PIECE_DESTROY_ONE = 0.07f;
    [Tooltip("Время уничтожения фишки")]
    public float TIME_PIECE_DESTROY_TWO = 0.2f;
    [Tooltip(" Время за которое фишка падает на одну клетку без ускорения")]
    public float TIME_PIECE_FALL = 0.17f;
    [Tooltip("Время полета светляка до цели")]
    public float TIME_BOOSTER_FLY = 1.5f;
    [Tooltip("Время за которое действие бустера пролетает расстояние в одну клетку")]
    public float BOOSTER_SPEED = 0.04f;
    [Tooltip("Время между последней активностью и шаффлом")]
    public float SHUFFLE_DELAY = 1.25f;
    [Tooltip("Время решафла")]
    public float TIME_SHUFFLE = 0.68f;
    [Tooltip("Задержка между активностью и показом хинта")]
    public float HINT_DELAY = 3f;
    [Tooltip("Время между анимациями показа хинта")]
    public float DELAY_BETWEEN_HINTS = 1.5f;
    [Tooltip("Время между анимациями показа хинта")]
    public float DELAY_MEGA = 0.1f;
    [Tooltip("Максимальная количество жизней при добавлении от таймера")]
    public int MAX_HEALTH = 5;
    [Tooltip("Время восстановления жизни в минутах")]
    public int TIME_RECOVERY_HEALTH = 30;
}
