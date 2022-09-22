using System.Collections.Generic;

namespace UI_WBF.AutomationElements.Grid.ElementGrid
{
    public abstract class AbstractGridRow<TGridRow> : IGridRow<TGridRow>
        where TGridRow : AbstractGridCell
    {
        #region Properties
        
        public abstract IList<TGridRow> Cells { get; }

        #endregion
    }
}
