using System.Collections.Generic;
using Castle.Windsor;
using Glass.Mapper.Configuration;
using Glass.Mapper.Umb.CastleWindsor;
using Glass.Mapper.Umb.Configuration.Attributes;

namespace AffordableInterior.CMS.Web.App_Start
{
    public static class GlassMapperUmbCustom
    {
		public static void CastleConfig(IWindsorContainer container){
			var config = new Config();
			container.Install(new UmbracoInstaller(config));
		}

		public static IConfigurationLoader[] GlassLoaders(){
            var attributes = new UmbracoAttributeConfigurationLoader("AffordableInterior.CMS.Web");
			
			return new IConfigurationLoader[]{attributes};
		}
    }
}
