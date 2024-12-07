namespace DiscontMarket.ApiModels.DTO.BaseDTOs
{
    public class FilterDTO
    {
        public string Title { get; set; }
        public List<OptionDTO> Options { get; set; }
    }
    public class OptionDTO
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class FilterCategoryDTO
    {
        public List<FilterDTO> Filters { get; set; }
    }
}
