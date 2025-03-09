using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.DTO
{
    public class CategoryTreeDto
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Depth { get; set; }
        public List<CategoryDetailsDto> Details { get; set; } = new List<CategoryDetailsDto>();
        public List<CategoryTreeDto> Children { get; set; } = new List<CategoryTreeDto>();
    }

    public class CategoryDetailsDto
    {
        public int DetailId { get; set; }
        public int CategoryId { get; set; } // dùng để ghép nối với CategoryTreeDto
        public string DetailName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

}