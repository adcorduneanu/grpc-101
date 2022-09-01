namespace TestServer.Core.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableExtensions
	{
		public static HashSet<TK> ToHashSet<T, TK>(this IEnumerable<T> items, Func<T, TK> action)
		{
			return new HashSet<TK>(items.Select(action));
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
			foreach(var item in items)
            {
				action(item);
            }
        }
	}
}
