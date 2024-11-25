using DiscontMarket.ApiModels.DTO.EntityDTOs.Product;
using DiscontMarket.ApiModels.Responce.Helpers;
using DiscontMarket.ApiModels.Responce.Interfaces;
using DiscontMarket.DAL.Repository.Interfaces;
using DiscontMarket.Domain.Models.Abstractions.LinkEntities;
using DiscontMarket.Domain.Models.Entities;
using DiscontMarket.Domain.Models.Enums;
using DiscontMarket.Services.Helpers.Filter;
using DiscontMarket.Services.Services.Interfaces;
using DiscontMarket.Validation;

namespace DiscontMarket.Services.Services.Implementations
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<Brand> _brandRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<AttributeEntity> _attributeRepository;

        public ProductService(IProductRepository productRepository, IBaseRepository<Brand> brandRepository,
            IBaseRepository<Category> categoryRepository, IBaseRepository<AttributeEntity> attributeRepository) : base(productRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _attributeRepository = attributeRepository;
        }

        public IBaseResponse<Product> CreateProduct(CreateProductDTO entityDTO)
        {
            try
            {
                ObjectValidator<CreateProductDTO>.CheckIsNotNullObject(entityDTO);

                var brand = _brandRepository
                    .GetAll()
                    .Where(x => x.Name.Equals(entityDTO.BrandName))
                    .FirstOrDefault();

                ObjectValidator<Brand>.CheckIsNotNullObject(brand);

                var categories = _categoryRepository.GetAll()
                    .Where(x => x.Name.Equals(entityDTO.CategoryName)).ToList();

                ObjectValidator<List<Category>>.CheckIsNotNullObject(categories);

                var attributes = new List<AttributeEntity>();
                foreach (var attributeName in entityDTO.AttributeNames)
                {
                    if (_attributeRepository.GetAll().Any(a => a.Name.Equals(attributeName)))
                        attributes
                            .Add(_attributeRepository
                            .GetAll()
                            .Where(a => a.Name.Equals(attributeName))
                            .First());
                }

                if (attributes.Count == 0)
                {
                    throw new Exception($"not found atributes");
                }


                Availability availability;
                if (!Enum.TryParse(entityDTO.Availability, out availability))
                {
                    throw new Exception($"not found availability {entityDTO.Availability}");
                }

                availability = (Availability)Enum.Parse(typeof(Availability), entityDTO.Availability);


                ProductStatus productStatus;
                if (!Enum.TryParse(entityDTO.Status, out productStatus))
                {
                    throw new Exception($"not found product status {entityDTO.Status}");
                }

                productStatus = (ProductStatus)Enum.Parse(typeof(Availability), entityDTO.Availability);


                var entity = new Product
                {
                    ProductName = entityDTO.ProductName,
                    Price = entityDTO.Price,
                    Quantity = entityDTO.Quantity,
                    ImagePath = entityDTO.ImagePath,
                    Availability = availability,
                    Status = productStatus,
                    Brand = brand,
                    Categories = categories,
                    ProductAttributes = attributes.Select(attribute => new ProductAttribute
                    {
                        Attribute = attribute
                    }).ToList()
                };

                _productRepository.Create(entity);

                return ResponseFactory<Product>.CreateSuccessResponse(entity);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<Product>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<Product>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<IEnumerable<Product>> GetAllProducts(FilterProductDTO filterProductDTO, SortTypes? sortType)
        {
            try
            {
                var filter = FilterHelper.CreateProductFilter(filterProductDTO);

                var entities = _productRepository.GetFilteredProducts(filter);
                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

                entities = SortHelper.SortProducts(entities, sortType);

                return ResponseFactory<IEnumerable<Product>>.CreateSuccessResponse(entities);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<Product>>.CreateErrorResponse(ex);

            }

        }
    }
}
