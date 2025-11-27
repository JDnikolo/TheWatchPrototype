namespace Boxing
{
	public interface IRef<out T>
	{
		T GetValue();
	}
}