using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum RowDefinition
    {
        First,
        Second,
        Third,
        Fourth,
    }


    public static class RowDefinitionUtils
    {
        private static RowDefinition[] s_arAll = new RowDefinition[] {
            RowDefinition.First, RowDefinition.Second,
            RowDefinition.Third, RowDefinition.Fourth };

        public static RowDefinition[] All
            => s_arAll;

        public static RowDefinition Any
            => s_arAll.Random();

        public static IEnumerable<RowDefinition> EnumerateAll(RowDefinition _excluding)
        {
            foreach (var definition in All.Where(x => x != _excluding))
                yield return definition;
        }

        public static IEnumerable<RowDefinition> EnumerateAll(RowDefinition[] _arExcluding)
        {
            foreach (var definition in All.Where(x => !_arExcluding.Contains(x)))
                yield return definition;
        }

        public static RowDefinition GetAny(RowDefinition _excluding)
        {
            return EnumerateAll(_excluding).Random();
        }

        public static bool TryOffset(RowDefinition _eRow, HorizontalMovement _eOffset, out RowDefinition _eRowOffsetted)
        {
            switch (_eRow)
            {
                case RowDefinition.First:
                    switch (_eOffset)
                    {
                        case HorizontalMovement.None:
                            _eRowOffsetted = RowDefinition.First;
                            return true;
                        case HorizontalMovement.OneRight:
                            _eRowOffsetted = RowDefinition.Second;
                            return true;
                        case HorizontalMovement.TwoRight:
                            _eRowOffsetted = RowDefinition.Third;
                            return true;
                        case HorizontalMovement.ThreeRight:
                            _eRowOffsetted = RowDefinition.Fourth;
                            return true;
                        default:
                        case HorizontalMovement.OneLeft:
                        case HorizontalMovement.TwoLeft:
                        case HorizontalMovement.ThreeLeft:
                            _eRowOffsetted = default;
                            return false;
                    }
                case RowDefinition.Second:
                    switch (_eOffset)
                    {
                        case HorizontalMovement.None:
                            _eRowOffsetted = RowDefinition.Second;
                            return true;
                        case HorizontalMovement.OneRight:
                            _eRowOffsetted = RowDefinition.Third;
                            return true;
                        case HorizontalMovement.TwoRight:
                            _eRowOffsetted = RowDefinition.Fourth;
                            return true;
                        case HorizontalMovement.OneLeft:
                            _eRowOffsetted = RowDefinition.First;
                            return true;
                        default:
                        case HorizontalMovement.TwoLeft:
                        case HorizontalMovement.ThreeLeft:
                        case HorizontalMovement.ThreeRight:
                            _eRowOffsetted = default;
                            return false;
                    }
                case RowDefinition.Third:
                    switch (_eOffset)
                    {
                        case HorizontalMovement.None:
                            _eRowOffsetted = RowDefinition.Third;
                            return true;
                        case HorizontalMovement.OneRight:
                            _eRowOffsetted = RowDefinition.Fourth;
                            return true;
                        case HorizontalMovement.OneLeft:
                            _eRowOffsetted = RowDefinition.Second;
                            return true;
                        case HorizontalMovement.TwoLeft:
                            _eRowOffsetted = RowDefinition.First;
                            return true;
                        default:
                        case HorizontalMovement.ThreeLeft:
                        case HorizontalMovement.TwoRight:
                        case HorizontalMovement.ThreeRight:
                            _eRowOffsetted = default;
                            return false;
                    }
                case RowDefinition.Fourth:
                    switch (_eOffset)
                    {
                        case HorizontalMovement.None:
                            _eRowOffsetted = RowDefinition.Fourth;
                            return true;
                        case HorizontalMovement.OneLeft:
                            _eRowOffsetted = RowDefinition.Third;
                            return true;
                        case HorizontalMovement.TwoLeft:
                            _eRowOffsetted = RowDefinition.Second;
                            return true;
                        case HorizontalMovement.ThreeLeft:
                            _eRowOffsetted = RowDefinition.First;
                            return true;
                        default:
                        case HorizontalMovement.OneRight:
                        case HorizontalMovement.TwoRight:
                        case HorizontalMovement.ThreeRight:
                            _eRowOffsetted = default;
                            return false;
                    }
                default:
                    _eRowOffsetted = default;
                    return false;
            }
        }
    }
}
