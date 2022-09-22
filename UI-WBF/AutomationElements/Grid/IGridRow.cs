using System.Collections.Generic;

namespace UI_WBF.AutomationElements.Grid
{
    public interface IGridRow<TGridCell>
        where TGridCell : IGridCell
    {
        #region Properties

        IList<TGridCell> Cells { get; }

        #endregion
    }
}
