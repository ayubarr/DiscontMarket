using DiscontMarket.ApiModels.DTO.BaseDTOs;

namespace DiscontMarket.ApiModels.DTO.EntityDTOs.User;

public class UserData : BaseDTO
{
    public string techLink { get; set; }
    public string vkLink { get; set; }
    public string tgLink { get; set; }
    public string wtLink { get; set; }
    public string time { get; set; }
    public string phoneNumber { get; set; }
    public string returnText { get; set; }
    public string textAdress { get; set; }
    public string hrefAdress { get; set; }
    public string hrefmapAdress { get; set; }
    public string textInfo { get; set; }
}