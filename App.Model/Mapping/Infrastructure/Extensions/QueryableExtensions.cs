using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace App.Model.Mapping.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///     Extension method to project from a queryable using the provided mapping engine 
        /// </summary>
        /// <remarks> Projections are only calculated once and cached </remarks>
        /// <typeparam name="TDestination"> Destination type </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(Mapper.Configuration, null, membersToExpand);
        }

        /// <summary>
        ///     Projects the source type to the destination type given the mapping configuration 
        /// </summary>
        /// <typeparam name="TDestination"> Destination type to map to </typeparam>
        /// <param name="source">          Queryable source </param>
        /// <param name="parameters">     
        ///     Optional parameter object for parameterized mapping expressions
        /// </param>
        /// <param name="membersToExpand"> Explicit members to expand </param>
        /// <returns>
        ///     Queryable result, use queryable extension methods to project and execute result
        /// </returns>
        public static IQueryable<TDestination> QueryTo<TDestination>(this IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            return source.ProjectTo<TDestination>(Mapper.Configuration, parameters, membersToExpand);
        }
    }
}
