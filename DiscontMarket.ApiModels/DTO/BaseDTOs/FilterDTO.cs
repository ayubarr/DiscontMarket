namespace DiscontMarket.ApiModels.DTO.BaseDTOs
{
    public class FilterDTO
    {
        public string Title { get; set; }
        public List<FilterOptionDTO> Options { get; set; }
    }
    public class FilterOptionDTO
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class CategoryFiltersDTO
    {
        public List<FilterDTO> Filters { get; set; }
    }
}
