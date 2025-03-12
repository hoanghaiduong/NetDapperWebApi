using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetDapperWebApi.DTO.Creates
{
    public class CreateBookingDTO : BookingDTO
    {
        [JsonIgnore]
        public new int? Status { get; set; }
        public List<CreateBookingRoomTypesDTO> BookingRoomTypes { get; set; }
    }
}