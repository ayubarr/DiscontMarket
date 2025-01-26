using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.User;

public class AdressData : BaseDTO
{
    public string? textAdress { get; set; }
        
    public string? hrefAdress { get; set; }
        
    public string? hrefmapAdress { get; set; }
}