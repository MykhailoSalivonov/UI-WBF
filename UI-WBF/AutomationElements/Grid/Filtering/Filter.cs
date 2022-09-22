using System;

namespace UI_WBF.AutomationElements.Grid.Filtering
{
    internal class Filter
    {
        #region Constructors

        internal Filter(Type type, string column, string value)
        {
            Type = type;
            Column = column;
            Value = value;
        }

        #endregion

        #region Proterties

        internal Type Type { get; private set; }

        internal string Column { get; }

        internal string Value { get; }

        #endregion

        #region Private Methods

        internal void ConvertToOppositeType()
        {
            switch (Type)
            {
                case Type.Contains:
                    Type = Type.DoesNotContain;
                    break;
                case Type.Equals:
                    Type = Type.DoesNotEqual;
                    break;
                default:
                    throw new NotImplementedException($"This filtering type {Type} has not been implemented yet.");
            }
        }

        #endregion
    }
}
