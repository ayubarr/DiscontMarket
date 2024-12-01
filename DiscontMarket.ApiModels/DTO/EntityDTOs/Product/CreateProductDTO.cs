using DiscontMarket.ApiModels.DTO.BaseDTOs;
using System.ComponentModel.DataAnnotations;


namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateProductDTO : BaseDTO
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public uint Quantity { get; set; }
        public List<string> images { get; set; }
        public string description { get; set; }
        public string fullDescription { get; set; }



        [Required(ErrorMessage = "Availability is required")]
        public string availability { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string status { get; set; }

        [Required(ErrorMessage = "Brand Name is required")]
        public string brandname { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string categoryname { get; set; }

        [Required(ErrorMessage = "Attribute Names is required")]
        public List<string> characteristics { get; set; }
    }
}
