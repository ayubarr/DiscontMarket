using DiscontMarket.Domain.Models.Abstractions.BaseEntities;
using DiscontMarket.Validation;

namespace DiscontMarket.Services.Helpers.Mapping
{
    internal class MapperHelper<Tmodel, T>
        where Tmodel : class
        where T : BaseEntity
    {
        /// <summary>
        /// Maps a collection of model objects to a collection of entity objects.
        /// </summary>
        /// <param name="sourceCollection">The source collection of model objects.</param>
        /// <returns>The mapped collection of entity objects.</returns>
        public static IEnumerable<T> Map(IEnumerable<Tmodel> sourceCollection)
        {
            ObjectValidator<IEnumerable<Tmodel>>.CheckIsNotNullObject(sourceCollection);

            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<IEnumerable<T>>(sourceCollection);
        }

        /// <summary>
        /// Maps a model object to an entity object.
        /// </summary>
        /// <param name="source">The source model object.</param>
        /// <returns>The mapped entity object.</returns>
        public static T Map(Tmodel source)
        {
            ObjectValidator<Tmodel>.CheckIsNotNullObject(source);
            var mapper = AutoMapperConfig<Tmodel, T>.Initialize();
            return mapper.Map<T>(source);
        }
    }
}
