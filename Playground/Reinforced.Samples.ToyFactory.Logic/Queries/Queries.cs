﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Features.Orm.PrimaryKey;
using Reinforced.Tecture.Features.Orm.Queries;

namespace Reinforced.Samples.ToyFactory.Logic.Queries
{
    public static class Queries
    {
        public static T ById<T>(this IQueryFor<T> q, int id)
            where T : IEntity
        {
            var r = q.All.Describe($"Get {typeof(T).Name} by Id #{id}").Where(x => x.Id == id);
            return r.FirstOrDefault();
        }

        public static P ById<T, P>(this IQueryFor<T> q, int id, Expression<Func<T, P>> projection)
            where T : IEntity
        {
            var r = q.All.Describe($"Get projection of {typeof(T).Name} by Id #{id}").Where(x => x.Id == id).Select(projection);
            return r.FirstOrDefault();
        }

        public static T ByIdRequired<T>(this IQueryFor<T> q, int id)
            where T : class, IEntity
        {
            var r = q.All.Describe($"Get {typeof(T).Name} by Id #{id} (required)").Where(x => x.Id == id);
            var result = r.FirstOrDefault();
            if (result == null)
            {
                throw new Exception($"Cannot find {typeof(T).Name} with Id {id}");
            }

            return result;
        }

        public static P ByIdRequired<T, P>(this IQueryFor<T> q, int id, Expression<Func<T, P>> projection)
            where T : class, IEntity
            where P : class
        {
            var r = q.All.Describe($"Get projection of {typeof(T).Name} by Id #{id} (required)")
                    .Where(x => x.Id == id)
                    .Select(projection);
            var result = r.FirstOrDefault();
            if (result == null)
            {
                throw new Exception($"Cannot find {typeof(T).Name} with Id {id}");
            }

            return result;
        }
    }
}