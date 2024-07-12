using Phwitti.PlayingCards;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum ColumnDefinition
    {
        First,
        Second,
        Third,
        Fourth,
    }

    public static class ColumnDefinitionUtils
    {
        private static ColumnDefinition[] s_arAll = new ColumnDefinition[] {
            ColumnDefinition.First, ColumnDefinition.Second,
            ColumnDefinition.Third, ColumnDefinition.Fourth };

        public static ColumnDefinition[] All
            => s_arAll;

        public static ColumnDefinition Any
            => s_arAll.Random();

        public static IEnumerable<ColumnDefinition> EnumerateAll(ColumnDefinition _excluding)
        {
            foreach (var definition in All.Where(x => x != _excluding))
                yield return definition;
        }

        public static IEnumerable<ColumnDefinition> EnumerateAll(ColumnDefinition[] _arExcluding)
        {
            foreach (var definition in All.Where(x => !_arExcluding.Contains(x)))
                yield return definition;
        }

        public static ColumnDefinition GetAny(ColumnDefinition _excluding)
        {
            return EnumerateAll(_excluding).Random();
        }

        public static bool TryOffset(ColumnDefinition _eColumn, VerticalMovement _eOffset, out ColumnDefinition _eColumnOffsetted)
        {
            switch (_eColumn)
            {
                case ColumnDefinition.First:
                    switch (_eOffset)
                    {
                        case VerticalMovement.None:
                            _eColumnOffsetted = ColumnDefinition.First;
                            return true;
                        case VerticalMovement.OneDown:
                            _eColumnOffsetted = ColumnDefinition.Second;
                            return true;
                        case VerticalMovement.TwoDown:
                            _eColumnOffsetted = ColumnDefinition.Third;
                            return true;
                        case VerticalMovement.ThreeDown:
                            _eColumnOffsetted = ColumnDefinition.Fourth;
                            return true;
                        default:
                        case VerticalMovement.ThreeUp:
                        case VerticalMovement.TwoUp:
                        case VerticalMovement.OneUp:
                            _eColumnOffsetted = default;
                            return false;
                    }
                case ColumnDefinition.Second:
                    switch (_eOffset)
                    {
                        case VerticalMovement.None:
                            _eColumnOffsetted = ColumnDefinition.Second;
                            return true;
                        case VerticalMovement.OneDown:
                            _eColumnOffsetted = ColumnDefinition.Third;
                            return true;
                        case VerticalMovement.TwoDown:
                            _eColumnOffsetted = ColumnDefinition.Fourth;
                            return true;
                        case VerticalMovement.OneUp:
                            _eColumnOffsetted = ColumnDefinition.First;
                            return true;
                        default:
                        case VerticalMovement.ThreeUp:
                        case VerticalMovement.ThreeDown:
                        case VerticalMovement.TwoUp:
                            _eColumnOffsetted = default;
                            return false;
                    }
                case ColumnDefinition.Third:
                    switch (_eOffset)
                    {
                        case VerticalMovement.None:
                            _eColumnOffsetted = ColumnDefinition.Third;
                            return true;
                        case VerticalMovement.OneDown:
                            _eColumnOffsetted = ColumnDefinition.Fourth;
                            return true;
                        case VerticalMovement.OneUp:
                            _eColumnOffsetted = ColumnDefinition.Second;
                            return true;
                        case VerticalMovement.TwoUp:
                            _eColumnOffsetted = ColumnDefinition.First;
                            return true;
                        default:
                        case VerticalMovement.ThreeUp:
                        case VerticalMovement.TwoDown:
                        case VerticalMovement.ThreeDown:
                            _eColumnOffsetted = default;
                            return false;
                    }
                case ColumnDefinition.Fourth:
                    switch (_eOffset)
                    {
                        case VerticalMovement.None:
                            _eColumnOffsetted = ColumnDefinition.Fourth;
                            return true;
                        case VerticalMovement.OneUp:
                            _eColumnOffsetted = ColumnDefinition.Third;
                            return true;
                        case VerticalMovement.TwoUp:
                            _eColumnOffsetted = ColumnDefinition.Second;
                            return true;
                        case VerticalMovement.ThreeUp:
                            _eColumnOffsetted = ColumnDefinition.First;
                            return true;
                        default:
                        case VerticalMovement.OneDown:
                        case VerticalMovement.TwoDown:
                        case VerticalMovement.ThreeDown:
                            _eColumnOffsetted = default;
                            return false;
                    }
                default:
                    _eColumnOffsetted = default;
                    return false;
            }
        }
    }
}
