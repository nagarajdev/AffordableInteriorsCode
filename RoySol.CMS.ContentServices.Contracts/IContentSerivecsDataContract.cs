
namespace RoySol.CMS.ContentServices.Contracts
{
    public interface IContentSerivecsDataContract
    {
        /// <summary>
        /// Get current rendering item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetCurrentItem<T>() where T : class;

        /// <summary>
        /// Get current rendering item by path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetItem<T>(int id) where T : class;

    }
}
