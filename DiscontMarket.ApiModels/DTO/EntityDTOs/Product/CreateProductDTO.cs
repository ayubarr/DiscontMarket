using DiscontMarket.ApiModels.DTO.BaseDTOs;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Brend;
using DiscontMarket.ApiModels.DTO.EntityDTOs.Category;
using System.ComponentModel.DataAnnotations;


namespace DiscontMarket.ApiModels.DTO.EntityDTOs.Product
{
    public class CreateProductDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public string ImagePath { get; set; }



        [Required(ErrorMessage = "Availability is required")]
        public string Availability { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Brand Name is required")]
        public string BrandName{ get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Attribute Names is required")]
        public List<string> AttributeNames { get; set; }

        //Пользователь создавший продукт
        public uint? UserID { get; set; } = 1;
    }
}
