using System;
using System.Collections.Generic;
using System.Linq;
using UI_WBF.AutomationElements.Grid.Filtering;

namespace UI_WBF.AutomationElements.Grid
{
    public interface IGrid<TGridHeader, TGridRow, TGridCell>
        where TGridHeader : IGridHeader
        where TGridRow : IGridRow<TGridCell>
        where TGridCell : IGridCell
    {
        #region Properties

        IList<TGridHeader> Headers { get; }

        IList<TGridRow> Rows { get; }

        int ColumnCount { get; }

        int RowCount { get; }

        #endregion

        #region Methods

        IList<string> GetColumnValues(string column)
        {
            IList<string> columnValues = new List<string>();
            int columnIndex = GetColumnIndex(column);

            foreach(var row in Rows)
            {
                columnValues.Add(row.Cells[columnIndex].Value);
            }

            return columnValues;
        }

        bool IsColumnContains(string column, string value)
        {
            return GetColumnValues(column).Any(i => i == value);
        }

        TGridCell GetCell(string column, By by);

        IList<TGridCell> GetCells(string column, By by);

        #endregion

        #region Protected Methods

        int GetColumnIndex(string columnName)
        {
            string[] headersNames = Headers.Select(i => i.ColumnName).ToArray();
            int index = Array.IndexOf(headersNames, columnName);

            if (index == -1)
                throw new ArgumentException($"Grid doesn't contain column '{columnName}'");

            return index;
        }

        #endregion
    }
}
