using UI_WBF.AutomationElements.Grid.Filtering;

namespace UI_WBF.AutomationElements.Grid.ElementGrid
{
    public abstract class AbstractGrid<TGridHeader, TGridRow, TGridCell> : IGrid<TGridHeader, TGridRow, TGridCell>
        where TGridHeader : AbstractHeader
        where TGridRow : AbstractGridRow<TGridCell>
        where TGridCell : AbstractGridCell
    {
        #region Properties

        public abstract IList<TGridHeader> Headers { get; }

        public abstract IList<TGridRow> Rows { get; }

        public abstract int ColumnCount { get; }

        public abstract int RowCount { get; }

        #endregion

        public TGridCell GetCell(string column, By by)
        {
            return GetCells(column, by).First();
        }

        public IList<TGridCell> GetCells(string column, By by)
        {
            IList<int> previusRowIntexes = null;
            int columnIndex;

            using (by)
            {
                columnIndex = Grid.GetColumnIndex(column);

                foreach (Filter filterBy in by.Filters)
                {
                    IList<int> rowIndexes = GetRowIndexesByFilter(filterBy);

                    if (previusRowIntexes == null)
                        previusRowIntexes = rowIndexes;
                    else if (rowIndexes.Count != 0 && previusRowIntexes.Count != 0)
                        previusRowIntexes = previusRowIntexes.Intersect(rowIndexes).ToList();
                    else
                        throw new ArgumentException($"Any data by current filter has not been found.");
                }
            }

            if(previusRowIntexes == null || previusRowIntexes.Count == 0)
                throw new ArgumentException($"Any data by current filter has not been found.");

            return previusRowIntexes.Select(i => Rows[i].Cells[columnIndex]).ToList();
        }

        internal IList<int> GetRowIndexesByFilter(Filter filter)
        {
            int columnIndex = Grid.GetColumnIndex(filter.Column);
            List<int> rowIndexes = new List<int>();

            for(int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                string rowValue = Rows[rowIndex].Cells[columnIndex].Value;

                switch (filter.Type)
                {
                    case Filtering.Type.Equals:
                        if(rowValue == filter.Value)
                            rowIndexes.Add(rowIndex);
                        break;
                    
                    case Filtering.Type.Contains:
                        if (rowValue.Contains(filter.Value))
                            rowIndexes.Add(rowIndex);
                        break;

                    case Filtering.Type.DoesNotEqual:
                        if (rowValue != filter.Value)
                            rowIndexes.Add(rowIndex);
                        break;

                    case Filtering.Type.DoesNotContain:
                        if (!rowValue.Contains(filter.Value))
                            rowIndexes.Add(rowIndex);
                        break;
                    
                    default:
                        throw new NotImplementedException($"This filtering type {filter.Type} has not been implemented yet.");
                }
            }

            return rowIndexes;
        }

        IGrid<TGridHeader, TGridRow, TGridCell> Grid => this;
    }
}
