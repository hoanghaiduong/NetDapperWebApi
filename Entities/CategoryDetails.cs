
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class CategoryDetails : BaseEntity<int>
    {

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Category Category { get; set; } = null!;

        [JsonIgnore]
        public int HotelId { get; set; }
        [JsonIgnore]
        public int RoomTypeId { get; set; }

    }
}