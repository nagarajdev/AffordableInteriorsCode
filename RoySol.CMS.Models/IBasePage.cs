using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Umb.Configuration;
using Glass.Mapper.Umb.Configuration.Attributes;

namespace RoySol.CMS.Models
{
    [UmbracoType(AutoMap = true)]
    public interface IBasePage
    {
        /// <summary>
        /// Item Id
        /// </summary>
        [UmbracoId]
        int Id { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        [UmbracoInfo(UmbracoInfoType.ContentTypeName)]
        string DocumentType { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        [UmbracoInfo(UmbracoInfoType.Name)]
        string Name { get; set; }

        /// <summary>
        /// Item Path
        /// </summary>
        [UmbracoInfo(UmbracoInfoType.Path)]
        string ItemPath { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        [UmbracoInfo(UmbracoInfoType.Creator)]
        string Creator { get; set; }

        /// <summary>
        /// CreateDate
        /// </summary>
        [UmbracoInfo(UmbracoInfoType.CreateDate)]
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Childrens Items
        /// </summary>
        [UmbracoChildren(InferType = true)]
        IEnumerable<IBasePage> ChildrensItems { get; set; }

    }
}
