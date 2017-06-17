namespace AssemblyVisualizer.Common
{
	internal interface ICanBeVirtual
	{
		bool IsVirtual { get; }
		bool IsOverride { get; }
	}
}