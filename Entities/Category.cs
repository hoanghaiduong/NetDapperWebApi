using System.Text.Json.Serialization;

namespace NetDapperWebApi.Entities
{
    public class Category : BaseEntity<int>
    {
        public int? ParentId { get; set; } // NULL nếu là cấp cao nhất
        public string Name { get; set; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Category> Children { get; set; } = [];
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryDetails> Details { get; set; } = [];
    }
}