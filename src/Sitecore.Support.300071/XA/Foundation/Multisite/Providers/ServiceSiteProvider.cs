using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Sites;

namespace Sitecore.Support.XA.Foundation.Multisite.Providers
{
    public class ServiceSiteProvider : Sitecore.XA.Foundation.Multisite.Providers.ServiceSiteProvider
    {
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        public override Site GetSite(string siteName)
        {
            Site site = ParentProvider?.GetSite(siteName);
            if (site != null && IsMatch(site))
            {
                ModifySiteProperties(site);
                return site;
            }
            return null;
        }

        public override SiteCollection GetSites()
        {
            SiteCollection siteCollection = new SiteCollection();
            if (ParentProvider != null)
            {
                List<Site> list = ParentProvider.GetSites().Where(IsMatch).ToList();
                list.ForEach(ModifySiteProperties);
                siteCollection.AddRange(list);
            }
            return siteCollection;
        }
    }
}