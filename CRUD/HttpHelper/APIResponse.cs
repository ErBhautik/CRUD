namespace CRUD.HttpHelper
{
	public class APIResponse<TResult>
	{
		public string Version { get; set; }
		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }
		public TResult Result { get; set; }
		public string RawResult { get; set; }

	}
}
