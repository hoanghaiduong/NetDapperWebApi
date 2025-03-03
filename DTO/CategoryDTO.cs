

using System.ComponentModel;
using System.Data;
using System.Text.Json.Serialization;

namespace NetDapperWebApi.DTO
{
    public class CategoryDTO
    {
       
        public int? ParentId { get; set; }  // NULL nếu là cấp cao nhất
        public string Name { get; set; } = string.Empty;
    }
}