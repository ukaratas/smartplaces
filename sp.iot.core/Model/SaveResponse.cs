using System;
using System.Collections.Generic;

namespace sp.iot.core
{
    public class SaveResponse<T> where T : BaseItem
    {
        public SaveResponse()
        {
            Actions = new List<SaveResponseAction>();
        }

        public void AddAction(string action)
        {
            Actions.Add(new SaveResponseAction { TimeStamp = DateTime.Now, Action = action });
        }

        public T Item { get; set; }

        public List<SaveResponseAction> Actions { get; set; }

        public SaveResponseType Status { get; set; }
    }
}
