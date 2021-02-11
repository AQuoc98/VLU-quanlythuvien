using System.Collections.Generic;

namespace CoreApp.Common.Models
{
    public class GridViewFilterControl
    {
        public GridViewFilterControl()
        {
            SelectData = new List<GridViewSelect>();
        }

        public IList<GridViewSelect> SelectData { get; set; }

        public void AddSelectData(List<GridViewSelect> data)
        {
            foreach (var item in data)
            {
                SelectData.Add(item);
            }
        }
    }
}
