using System;
using System.Collections.Generic;

namespace sp.iot.core
{
    public class SaveResponse<T>
    {
        public SaveResponse()
        {
            Actions = new List<SaveResponseAction>();
        }

        public void AddAction(string action)
        {
            Actions.Add(new SaveResponseAction { CreatedAt = DateTime.Now, Action = action });
        }

        public void AddAction(string action, SaveActionType type)
        {
            Actions.Add(new SaveResponseAction { CreatedAt = DateTime.Now, Action = action, Type = type });
        }

        public T Item { get; set; }

        public List<SaveResponseAction> Actions { get; set; }

        public SaveResponseType Status { get; set; }
    }

    public class SaveResponse
    {
        public SaveResponse()
        {
            Actions = new List<SaveResponseAction>();
        }

        public void AddAction(string action)
        {
            Actions.Add(new SaveResponseAction { CreatedAt = DateTime.Now, Action = action });
        }

        public List<SaveResponseAction> Actions { get; set; }

        public SaveResponseType Status { get; set; }
    }
}
