using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections;
using System.Linq.Expressions;

namespace FundRaising.Common.Mappings
{
    public static class MappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(AutoMapperConfig.config, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(AutoMapperConfig.config, parameters);
        }
        public static Destination To<Destination>(this object source) => AutoMapperConfig.mapper.Map<Destination>(source);
       // public static Destination To<Destination>(this object source) => Mapper.Map<Destination>(source);
        public static Destination To<Destination>(this object source, object destination) =>
            (Destination)AutoMapperConfig.mapper.Map(source, destination, source.GetType(), destination.GetType());

        public static Destination To<Source, Destination>(this Source source, Destination destination, Action<IMappingOperationOptions<Source, Destination>> options) =>
           AutoMapperConfig.mapper.Map(source, destination, options);

        public static IEnumerable<TDestination> MapCollection<TDestination>(this IEnumerable enumerable)
            => AutoMapperConfig.mapper.Map<IEnumerable<TDestination>>(enumerable);

		public static async Task<TDestination> To<TDestination>(this Task task)
        {
            var taskWithResult = task as dynamic;
            return AutoMapperConfig.mapper.Map<TDestination>(await taskWithResult);
        }
        public static async Task<IEnumerable<TDestination>> MapCollection<TDestination>(this Task task)
        {
            var taskWithResult = task as dynamic;

            if (!task.HasEnumerableResult())
            {
                dynamic destination = AutoMapperConfig.mapper.Map<TDestination>(await taskWithResult);
                return new List<TDestination> { destination };
            }

            return AutoMapperConfig.mapper.Map<IEnumerable<TDestination>>(await taskWithResult);
        }

        private static bool HasEnumerableResult(this Task task)
        {
            Type type = task.GetType();
            if (!type.IsGenericType)
            {
                throw new InvalidOperationException("Cannot map a void Task.");
            }

            Type[] genericArguments = type.GetGenericArguments();

            return typeof(IEnumerable).IsAssignableFrom(genericArguments.First());
        }
    }
}
