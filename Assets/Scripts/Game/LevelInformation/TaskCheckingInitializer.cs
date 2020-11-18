using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.Game
{
    public class TaskCheckingInitializer : MonoBehaviour
    {
        public static IBoardObjectChecking GetTaskCheking(TypeTask typeTask)
        {
            switch (typeTask)
            {
                case TypeTask.PieceRnd:
                    return new PieceGeneralChecking();
                case TypeTask.PieceRed:
                    return new PieceRedChecking();
                case TypeTask.PieceYellow:
                    return new PieceYellowChecking();
                case TypeTask.PieceGreen:
                    return new PieceGreenChecking();
                case TypeTask.PieceBlue:
                    return new PieceBlueChecking();
                case TypeTask.PieceOrange:
                    return new PieceOrangeChecking();
                case TypeTask.PieceAcorn:
                    return new PieceAcornChecking();
            }

            return new BoardObjectNullChecking();
        }
    }
}
