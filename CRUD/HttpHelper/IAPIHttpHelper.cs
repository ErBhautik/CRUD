namespace CRUD.HttpHelper
{
    public interface IAPIHttpHelper
    {
        public APIResponse<TResult> GetRequest<TResult>(string uri);
    }
}
