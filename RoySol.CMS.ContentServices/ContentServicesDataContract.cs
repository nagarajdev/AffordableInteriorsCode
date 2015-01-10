using System;
using Glass.Mapper.Umb;
using RoySol.CMS.ContentServices.Contracts;
using Umbraco.Core;
using Umbraco.Web; 
namespace RoySol.CMS.ContentServices
{
    public class ContentServicesDataContract : IContentSerivecsDataContract
    {
        /// <summary>
        /// UmbracoService
        /// </summary>
        readonly IUmbracoService UmbracoService = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public ContentServicesDataContract(IUmbracoService service)
        {
            this.UmbracoService = service;
        }

        /// <summary>
        /// GetCurrentItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetCurrentItem<T>() where T : class
        {
            return this.UmbracoService.GetHomeItem<T>();
        }

        /// <summary>
        /// GetItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetItem<T>(int id) where T : class
        {
            return this.UmbracoService.CreateType<T>(this.UmbracoService.ContentService.GetPublishedVersion(id));
        }

       
    }
}
