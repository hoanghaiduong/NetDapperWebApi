
using System.Text.Json.Serialization;

namespace NetDapperWebApi.Entities
{
    public class Amenities : BaseEntity<int>
    {

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Category Category { get; set; }
    }
}