using Microsoft.Azure.Mobile.Server;

namespace TatbikatApi.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}