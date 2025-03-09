

using System.Text.Json.Serialization;

namespace NetDapperWebApi.Entities
{
    public class RoomTypeCategory
    {
        public int RoomTypeId { get; set; }
        public int CategoryDetailId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual RoomType? RoomType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual CategoryDetails? CategoryDetails { get; set; }
    }
}