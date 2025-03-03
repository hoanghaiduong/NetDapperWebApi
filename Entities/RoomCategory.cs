

using System.Text.Json.Serialization;

namespace NetDapperWebApi.Entities
{
    public class RoomCategory
    {
        public int RoomId { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Room? Room { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Category? Category { get; set; }
    }
}