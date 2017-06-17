using AssemblyVisualizer.Model;

namespace AssemblyVisualizer.Common
{
	internal static class Extensions
	{
		public static bool IsVisibleOutside(this MemberInfo methodInfo)
		{
			return methodInfo.IsPublic || methodInfo.IsInternal || methodInfo.IsProtectedOrInternal;
		}
	}
}