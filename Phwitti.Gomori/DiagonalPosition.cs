using Phwitti.PlayingCards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Phwitti.Gomori
{
    public enum DiagonalPosition
    {
        First,
        Second,
        Third,
        Fourth
    }

    public static class DiagonalPositionUtils
    {
        private static DiagonalPosition[] s_arAll = new DiagonalPosition[] {
            DiagonalPosition.First, DiagonalPosition.Second,
            DiagonalPosition.Third, DiagonalPosition.Fourth };

        public static DiagonalPosition[] All
            => s_arAll;

        public static DiagonalPosition Any
            => s_arAll.Random();

        public static DiagonalPosition CreateFromFieldDefinition(
            FieldDefinition _fieldDefinition, DiagonalDefinition _eDiagonalDefinition)
        {
            switch (_eDiagonalDefinition)
            {
                case DiagonalDefinition.Ascending:
                    return CreateFromFieldDefinitionAscending(_fieldDefinition);
                case DiagonalDefinition.Descending:
                    return CreateFromFieldDefinitionDescending(_fieldDefinition);
                default:
                    throw new InvalidEnumArgumentException(
                        argumentName: nameof(_eDiagonalDefinition),
                        invalidValue: (int)_eDiagonalDefinition,
                        enumClass: typeof(DiagonalDefinition));
            }
        }

        //

        private static DiagonalPosition CreateFromFieldDefinitionAscending(
            FieldDefinition _fieldDefinition)
        {
            if (_fieldDefinition.Row == RowDefinition.Fourth && _fieldDefinition.Column == ColumnDefinition.First)
                return DiagonalPosition.First;
            else if (_fieldDefinition.Row == RowDefinition.Third && _fieldDefinition.Column == ColumnDefinition.Second)
                return DiagonalPosition.Second;
            else if (_fieldDefinition.Row == RowDefinition.Second && _fieldDefinition.Column == ColumnDefinition.Third)
                return DiagonalPosition.Third;
            else if (_fieldDefinition.Row == RowDefinition.First && _fieldDefinition.Column == ColumnDefinition.Fourth)
                return DiagonalPosition.Fourth;
            else
                throw new InvalidOperationException($"Can't map FieldDefinition with Row = {_fieldDefinition.Row}" +
                    $" and Column = {_fieldDefinition.Column} to Ascending DiagonalPosition");
        }

        private static DiagonalPosition CreateFromFieldDefinitionDescending(
            FieldDefinition _fieldDefinition)
        {
            if (_fieldDefinition.Row == RowDefinition.First && _fieldDefinition.Column == ColumnDefinition.First)
                return DiagonalPosition.First;
            else if (_fieldDefinition.Row == RowDefinition.Second && _fieldDefinition.Column == ColumnDefinition.Second)
                return DiagonalPosition.Second;
            else if (_fieldDefinition.Row == RowDefinition.Third && _fieldDefinition.Column == ColumnDefinition.Third)
                return DiagonalPosition.Third;
            else if (_fieldDefinition.Row == RowDefinition.Fourth && _fieldDefinition.Column == ColumnDefinition.Fourth)
                return DiagonalPosition.Fourth;
            else
                throw new InvalidOperationException($"Can't map FieldDefinition with Row = {_fieldDefinition.Row}" +
                    $" and Column = {_fieldDefinition.Column} to Descending DiagonalPosition");
        }

        public static IEnumerable<DiagonalPosition> EnumerateAll(DiagonalPosition _excluding)
        {
            foreach (var definition in All.Where(x => x != _excluding))
                yield return definition;
        }

        public static IEnumerable<DiagonalPosition> EnumerateAll(DiagonalPosition[] _arExcluding)
        {
            foreach (var definition in All.Where(x => !_arExcluding.Contains(x)))
                yield return definition;
        }

        public static DiagonalPosition GetAny(DiagonalPosition _excluding)
        {
            return EnumerateAll(_excluding).Random();
        }
    }
}
