public enum TypeBoardObject
{
    NULL = 0,

    PieceRnd = 1,
    PieceRed = 2,
    PieceYellow = 3,
    PieceGreen = 4,
    PieceBlue = 5,
    PieceOrange = 6,

    PieceAcornOne = 20,
    PieceAcornTwo = 21,
}

public enum TypeTask
{
    NULL = 0,

    PieceRnd = 1,
    PieceRed = 2,
    PieceYellow = 3,
    PieceGreen = 4,
    PieceBlue = 5,
    PieceOrange = 6,

    PieceAcorn = 50,
}

public enum TypeActions
{
    PieceDestroing,
    PieceBounce,
    PieceDelete,
    PieceHit,
    PieceMoving,
    PieceCreating,
    HitTile,
    BlockDestroing,
    BlockHit,
    Transport,
    BoosterHit,
    BoosterDelay
}

public enum DragState
{
    WAITING,
    DRAGGING_PIECE,
    SWAP_TWO_PIECES
}

public enum TypeSwap
{
    Pieces,
    PieceBooster,
    Boosters
}

public enum TileColorState
{
    YELLOW,
    RED,
    GREEN,
    CYAN,
}

public enum TypeSound
{
    ButtonClick
}
