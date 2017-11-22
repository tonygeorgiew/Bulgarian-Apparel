namespace Bulgarian_Apparel.Services.Logic
{
    using AutoMapper;
    using Bulgarian_Apparel.Data.Models;
    using Bulgarian_Apparel.Services.Logic.Contracts;
    using System.Collections.Generic;

    public class MappingService : IMappingService
    {
        private readonly IMapper mapper;

        public MappingService(IMapper mapper)
        {
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }
    }
}
