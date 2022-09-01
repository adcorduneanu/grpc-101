namespace TestServer.Core.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class EnumerableExtensions
	{
		//public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
		//{
		//    return new HashSet<T>(items);
		//}

		public static HashSet<TK> ToHashSet<T, TK>(this IEnumerable<T> items, Func<T, TK> action)
		{
			return new HashSet<TK>(items.Select(action));
		}
	}
}
