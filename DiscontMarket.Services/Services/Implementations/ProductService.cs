using DiscontMarket.ApiModels.DTO.EntityDTOs.Attribute;
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
        private readonly IBaseRepository<Image> _imageRepository;

        public ProductService(IProductRepository productRepository, IBaseRepository<Brand> brandRepository,
            IBaseRepository<Category> categoryRepository, IBaseRepository<AttributeEntity> attributeRepository, 
            IBaseRepository<Image> imageRepository) : base(productRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _attributeRepository = attributeRepository;
            _imageRepository = imageRepository;
        }
        private IBaseResponse<bool> UpdateProduct(Product entity)
        {
            try
            {
                ObjectValidator<Product>.CheckIsNotNullObject(entity);

                _productRepository.Update(entity);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<bool> CreateProduct(CreateProductDTO entityDTO)
        {
            try
            {
                ObjectValidator<CreateProductDTO>.CheckIsNotNullObject(entityDTO);

               
                var brand = _brandRepository
                    .GetAll()
                    .Where(x => x.Name.ToLower().Equals(entityDTO.brandname.ToLower()))
                    .FirstOrDefault();

                ObjectValidator<Brand>.CheckIsNotNullObject(brand);

                var category = _categoryRepository.GetAll()
                    .Where(x => x.Name.ToLower().Equals(entityDTO.categoryname.ToLower())).FirstOrDefault();

                ObjectValidator<Category>.CheckIsNotNullObject(category);

                var attributes = new List<AttributeEntity>();
                foreach (var attributeName in entityDTO.characteristics)
                {
                    if (_attributeRepository.GetAll().Any(a => a.Name.ToLower().Equals(attributeName.ToLower())))
                        attributes
                            .Add(_attributeRepository
                            .GetAll()
                            .Where(a => a.Name.ToLower().Equals(attributeName.ToLower()))
                            .First());
                }

                if (attributes.Count == 0)
                {
                    throw new Exception($"not found atributes");
                }


                Availability availability;
                if (!Enum.TryParse(entityDTO.availability, true, out availability))
                {
                    throw new Exception($"not found availability {entityDTO.availability}");
                }

                availability = (Availability)Enum.Parse(typeof(Availability), entityDTO.availability, true);


                ProductStatus productStatus;
                if (!Enum.TryParse(entityDTO.status, true, out productStatus))
                {
                    throw new Exception($"not found product status {entityDTO.status}");
                }

                productStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), entityDTO.status, true);

                var images = new List<Image>();
                string image = "";
                if (entityDTO.images.Count > 0)
                {
                    foreach (var path in entityDTO.images)
                    {
                        images.Add(new Image
                        {
                            Path = path,
                        });
                    }
                    image =  entityDTO.images.First();
                }

                var rating = Math.Round(4.1 + new Random().NextDouble() * 0.9, 1);

                var entity = new Product
                {
                    ProductName = entityDTO.title,
                    Price = entityDTO.price,
                    Quantity = entityDTO.Quantity,
                    IconPath = image,
                    Rating = rating,
                    Description = entityDTO.description,
                    FullDescription = entityDTO.fullDescription,
                    Availability = availability,
                    Status = productStatus,
                    Brand = brand,
                    Category = category,
                    ProductAttributes = attributes.Select(attribute => new ProductAttribute
                    {
                        Attribute = attribute
                    }).ToList(),
                    Images = images,  
                    UserID = 1,
                };


                if (_productRepository.GetAll().Any(p => p.ProductName.Equals(entityDTO.title))) return UpdateProduct(entity);
                
                _productRepository.Create(entity);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public IBaseResponse<bool> DeleteByProductName(string productName)
        {
            try
            {
                var product = _productRepository.GetAll().Where(p => p.ProductName.Equals(productName)).FirstOrDefault();

                ObjectValidator<Product>.CheckIsNotNullObject(product);

                _productRepository.Delete(product);
                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (Exception ex)
            {
                return ResponseFactory<bool>.CreateErrorResponse(ex);
            }
        }
        private string xyu = "";
        public IBaseResponse<IEnumerable<ProductDTO>> GetAllProducts(FilterProductDTO filterProductDTO, string? sortType)
        {
            try
            {
                var filter = FilterHelper.CreateProductFilter(filterProductDTO);

                var entities = _productRepository.GetFilteredProducts(filter);

                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

                entities = SortHelper.SortProducts(entities, sortType);


                var productDto = entities.Select(product => new ProductDTO
                {
                    id = product.ID,
                    productName = product.ProductName,
                    price = product.Price,
                    productAvailability = product.Availability.ToString(),
                    productStatus = product.Status.ToString(),
                    image = product.IconPath,
                    quantity = product.Quantity,
                    userid = product.UserID,
                    categoryid = product.CategoryID,
                    brendId = product.BrandID,
                }).AsEnumerable();


                return ResponseFactory<IEnumerable<ProductDTO>>.CreateSuccessResponse(productDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<ProductDTO>>.CreateErrorResponse(ex);
            }

        }

        public IBaseResponse<GetProductDTO> GetProductById(uint Id)
        {
            try
            {
                var product = _productRepository.GetById(Id);

                ObjectValidator<Product>.CheckIsNotNullObject(product);

                var productImages = _imageRepository
                    .GetAll()
                    .Where(i => i.ProductID == product.ID)
                    .Select(i => i.Path)
                    .ToList();

                // Получаем характеристики продукта через контейнер атрибутов
                var characteristics = _attributeRepository
                    .GetAll()
                    .Where(a => a.ProductAttributes!.Any(pa => pa.ProductID == product.ID))
                    .Select(a => new CharacteristicDTO()
                    {
                        Name = a.Type,
                        Value = a.NameTranslate
                    })
                    .Where(attr => !string.IsNullOrEmpty(attr.Name) && !string.IsNullOrEmpty(attr.Value)).ToList();

                var productDto = new GetProductDTO
                {
                    productId = (int)product.ID,
                    title =  product.ProductName,
                    rating = product.Rating,
                    description = product.Description,
                    price = product.Price,
                    characteristics = characteristics,
                    images = productImages,
                    fullDescription = product.FullDescription,

                };

                ObjectValidator<GetProductDTO>.CheckIsNotNullObject(productDto);

                return ResponseFactory<GetProductDTO>.CreateSuccessResponse(productDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory<GetProductDTO>.CreateErrorResponse(ex);

            }
        }

        public IBaseResponse<IEnumerable<GetProductDTO>> GetProductsByName(string name)
        {
            try
            {
                // Получаем список продуктов, соответствующих имени или содержащих имя в названии
                var products = _productRepository
                    .GetAll()
                    .Where(p => p.ProductName.ToLower().Contains(name.ToLower()))
                    .ToList();

                // Проверяем, что продукты найдены
                ObjectValidator<List<Product>>.CheckIsNotNullObject(products);

                // Создаем список DTO
                var productDtos = products.Select(product =>
                {
                    // Получаем изображения продукта
                    var productImages = _imageRepository
                        .GetAll()
                        .Where(i => i.ProductID == product.ID)
                        .Select(i => i.Path)
                        .ToList();

                    // Получаем характеристики продукта через контейнер атрибутов
                    var characteristics = _attributeRepository
                        .GetAll()
                        .Where(a => a.ProductAttributes!.Any(pa => pa.ProductID == product.ID))
                        .Select(a => new CharacteristicDTO
                        {
                            Name = a.Type,
                            Value = a.NameTranslate
                        })
                        .Where(attr => !string.IsNullOrEmpty(attr.Name) && !string.IsNullOrEmpty(attr.Value))
                        .ToList();

                    // Формируем DTO объекта
                    return new GetProductDTO
                    {
                        productId = (int)product.ID,
                        title = product.ProductName,
                        rating = product.Rating,
                        description = product.Description,
                        price = product.Price,
                        characteristics = characteristics,
                        images = productImages,
                        fullDescription = product.FullDescription
                    };
                }).ToList();

                // Проверяем, что DTO корректно создан
                ObjectValidator<IEnumerable<GetProductDTO>>.CheckIsNotNullObject(productDtos);

                // Возвращаем успешный ответ
                return ResponseFactory<IEnumerable<GetProductDTO>>.CreateSuccessResponse(productDtos);
            }
            catch (Exception ex)
            {
                // Возвращаем ошибочный ответ
                return ResponseFactory<IEnumerable<GetProductDTO>>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<IEnumerable<ProductDTO>> GetAllProductsNews()
        {
            try
            {
                const decimal NewsPrice = 14000;
                const int NewsCount = 12;

                var entities = _productRepository.GetAll()
                    .Where(p => p.Price >= NewsPrice)
                    .Take(NewsCount)
                    .ToList();

                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);


                var productDto = entities.Select(product => new ProductDTO
                {
                    id = product.ID,
                    productName = product.ProductName,
                    price = product.Price,
                    productAvailability = product.Availability.ToString(),
                    productStatus = product.Status.ToString(),
                    image = product.IconPath,
                    quantity = product.Quantity,
                    userid = product.UserID,
                    categoryid = product.CategoryID,
                    brendId = product.BrandID,
                }).AsEnumerable();


                return ResponseFactory<IEnumerable<ProductDTO>>.CreateSuccessResponse(productDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<ProductDTO>>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<IEnumerable<ProductDTO>> GetAllProductsHits()
        {
            try
            {
                const double HitRating = 4.5;
                const int HitsCount = 12;

                var entities = _productRepository.GetAll()
                    .Where(p => p.Rating >= HitRating) 
                    .Take(HitsCount) 
                    .ToList();

                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

              
                var productDto = entities.Select(product => new ProductDTO
                {
                    id = product.ID,
                    productName = product.ProductName,
                    price = product.Price,
                    productAvailability = product.Availability.ToString(),
                    productStatus = product.Status.ToString(),
                    image = product.IconPath,
                    quantity = product.Quantity,
                    userid = product.UserID,
                    categoryid = product.CategoryID,
                    brendId = product.BrandID,
                }).AsEnumerable();


                return ResponseFactory<IEnumerable<ProductDTO>>.CreateSuccessResponse(productDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<ProductDTO>>.CreateErrorResponse(ex);
            }
        }

        public IBaseResponse<IEnumerable<ProductDTO>> GetAllProductsByStatus( FilterProductDTO filterProductDTO, string? sortType, string status)
        {
            try
            {            
                ProductStatus productStatus;
                if (!Enum.TryParse(status, true, out productStatus))
                {
                    throw new Exception($"not found product status {status}");
                }

                productStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), status, true);

                var filter = FilterHelper.CreateProductFilterWitoutAttributes(filterProductDTO);

                var entities = _productRepository.GetFilteredProducts(filter)
                    .Where(p => p.Status.Equals(productStatus));

                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

                entities = SortHelper.SortProducts(entities, sortType);

            
                ObjectValidator<IEnumerable<Product>>.CheckIsNotNullObject(entities);

                var productDto = entities.Select(product => new ProductDTO
                {
                    id = product.ID,
                    productName = product.ProductName,
                    price = product.Price,
                    productAvailability = product.Availability.ToString(),
                    productStatus = product.Status.ToString(),
                    image = product.IconPath,
                    quantity = product.Quantity,
                    userid = product.UserID,
                    categoryid = product.CategoryID,
                    brendId = product.BrandID,
                }).AsEnumerable();


                return ResponseFactory<IEnumerable<ProductDTO>>.CreateSuccessResponse(productDto);
            }
            catch (Exception ex)
            {
                return ResponseFactory<IEnumerable<ProductDTO>>.CreateErrorResponse(ex);
            }
        }
    }
}
