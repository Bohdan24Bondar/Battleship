
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    [Serializable]
    public class RandomCoords
    {
        public const int MAP_SIZE = 10;
        public const int COUNT_OF_SHIPS = 10;
        public const int COUNT_OF_SHIPS_TYPE = 4;
        public const int COUNT_OF_DIRECTION = 4;
        public const int COUNT_OF_COORDS = 2;
        public const int SHIP_INJURED = 2;
        public const int SHIP_DEAD = 3;
        public const int SECCESSFUL_SHOT = 2;
        public const int TWO_SHOTS = 2;
        public const int FOUR_SHOTS = 4;
        public const int SIX_SHOTS = 6;
        public const int SEVEN_SHOTS = 7;
        public const int EIGTH_SHOTS = 8;
        public const int TEN_SHOTS = 10;
        public const int DIRECTION_COUNT = 9;

        #region GetRandomCooirdinatesMethod(Методы получения случайных координат)

        public static Random randCoord = new Random();

        public static void SearchRandomCoords(Sea playerMap)
        {
            bool wasShot;

            do
            {
                GetRandomCoords(playerMap);

                wasShot = playerMap.WasShot();

            } while (wasShot);

        }

        public static void GetRandomCoords(Sea map)
        {
            map.TargetCoordX = randCoord.Next() % MAP_SIZE;
            map.TargetCoordY = randCoord.Next() % MAP_SIZE;
        }

        public static ShotDirection[] GetLineDirectionForShoot()
        {
            ShotDirection[] directionsOrder = new ShotDirection[DIRECTION_COUNT];
            ShotDirection currentDirection = ShotDirection.NoneDirection;
            int chosenDirection = 0;

            for (int i = 0; i < Constants.TENTH_CELL; i++)
            {
                if (i < Constants.FIFTH_CELL)
                {
                    do
                    {
                        chosenDirection = randCoord.Next() % EIGTH_SHOTS;

                    } while (chosenDirection % TWO_SHOTS == 0);
                }
                else
                {
                    if ((i >= Constants.FIFTH_CELL) && (i < SEVEN_SHOTS))
                    {
                        do
                        {
                            chosenDirection = randCoord.Next() % EIGTH_SHOTS;

                        } while ((chosenDirection % TWO_SHOTS != 0) || (chosenDirection == 0)
                                || (chosenDirection == EIGTH_SHOTS));
                    }
                    else
                    {

                        if (randCoord.Next() % TWO_SHOTS == 0)
                        {
                            chosenDirection = EIGTH_SHOTS;
                        }
                        else
                        {
                            chosenDirection = 0;
                        }
                    }
                }

                if (chosenDirection == 0)
                {
                    chosenDirection = 1;
                }
                else
                {
                    chosenDirection = (int)Math.Pow(TWO_SHOTS, chosenDirection);
                }

                switch ((ShotDirection)chosenDirection)
                {
                    case ShotDirection.TwoCellWestNorth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.FourCellWestNorth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.SixCellWestNorth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.EightCellWestNorth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.Center:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.EightCellEastSouth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.SixCellEastSouth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.FourCellEastSouth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    case ShotDirection.TwoCellEastSouth:
                        CheckDirection(chosenDirection, directionsOrder, ref i, ref currentDirection);
                        break;
                    default:
                        break;
                }
            }

            return directionsOrder;
        }

        public static void CheckDirection(int chosenDirection, ShotDirection[] directionsOrder,
                ref int iterationNumber, ref ShotDirection currentDirection)
        {
            if (currentDirection.HasFlag((ShotDirection)chosenDirection))
            {
                iterationNumber--;
            }
            else
            {
                directionsOrder[iterationNumber] = (ShotDirection)chosenDirection;
                currentDirection += chosenDirection;
            }
        }

        public static Queue<Position> BuildShootsLine()
        {
            bool isFromTop;
            int countShootsInLine;
            Position startPosition;
            Queue<Position> plentyShoots = new Queue<Position>();

            ShotDirection[] directionsOrder = RandomCoords.GetLineDirectionForShoot();

            for (int i = 0; i < directionsOrder.Length; i++)
            {
                switch (directionsOrder[i])
                {
                    case ShotDirection.TwoCellWestNorth:
                        startPosition = new Position(Constants.SECOND_CELL, 0);
                        countShootsInLine = TWO_SHOTS;
                        isFromTop = false;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.FourCellWestNorth:
                        startPosition = new Position(0, Constants.FOURTH_CELL);
                        countShootsInLine = FOUR_SHOTS;
                        isFromTop = true;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.SixCellWestNorth:
                        startPosition = new Position(Constants.SIXTH_CELL, 0);
                        countShootsInLine = SIX_SHOTS;
                        isFromTop = false;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.EightCellWestNorth:
                        startPosition = new Position(0, Constants.EIGHTH_CELL);
                        countShootsInLine = EIGTH_SHOTS;
                        isFromTop = true;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.Center:
                        startPosition = new Position(Constants.TENTH_CELL, 0);
                        countShootsInLine = TEN_SHOTS;
                        isFromTop = false;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.EightCellEastSouth:
                        startPosition = new Position(Constants.THIRD_CELL, Constants.TENTH_CELL);
                        countShootsInLine = EIGTH_SHOTS;
                        isFromTop = true;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.SixCellEastSouth:
                        startPosition = new Position(Constants.TENTH_CELL, Constants.FIFTH_CELL);
                        countShootsInLine = SIX_SHOTS;
                        isFromTop = false;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.FourCellEastSouth:
                        startPosition = new Position(Constants.SEVENTH_CELL, Constants.TENTH_CELL);
                        countShootsInLine = FOUR_SHOTS;
                        isFromTop = true;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    case ShotDirection.TwoCellEastSouth:
                        startPosition = new Position(Constants.TENTH_CELL, Constants.NINTH_CELL);
                        countShootsInLine = TWO_SHOTS;
                        isFromTop = false;
                        CalculateCoordsForShoot(countShootsInLine, startPosition, isFromTop, ref plentyShoots);
                        break;
                    default:
                        break;
                }
            }

            return plentyShoots;

        }

        public static void CalculateCoordsForShoot(int countShootsInLine, Position startPosition,
                bool isFromTop, ref Queue<Position> plentyShoots)
        {
            for (int i = 0; i < countShootsInLine; i++)
            {
                plentyShoots.Enqueue(startPosition);

                if (isFromTop)
                {
                    startPosition.OY++;
                    startPosition.OX--;
                }
                else
                {
                    startPosition.OY--;
                    startPosition.OX++;
                }

            }
        }

        public static Direction GetRandomDirection()
        {
            return (Direction)randCoord.Next(1, 5);
        }

        #endregion
    }
}
