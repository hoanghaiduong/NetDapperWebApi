
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.Entities
{
    public class CategoryDetails : BaseEntity<int>
    {
        /// <summary>
        // 
        /// </summary>
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }=null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Category Category { get; set; }
    }
}