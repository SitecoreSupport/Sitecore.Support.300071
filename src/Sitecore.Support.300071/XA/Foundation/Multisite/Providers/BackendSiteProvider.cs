using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Sites;

namespace Sitecore.Support.XA.Foundation.Multisite.Providers
{
    public class BackendSiteProvider : Sitecore.XA.Foundation.Multisite.Providers.BackendSiteProvider
    {
        private string _supportParentProviderName;
        private SiteProvider _supportParentProvider;
        private SiteProvider SupportParentProvider
        {
            get
            {
                return _supportParentProvider ?? (_supportParentProvider = SiteManager.Providers[_supportParentProviderName]);
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            this._supportParentProviderName = config["inherits"] ?? string.Empty;
        }

        public override Site GetSite(string siteName)
        {
            Site site = this.SupportParentProvider?.GetSite(siteName);
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
            if (this.SupportParentProvider != null)
            {
                List<Site> list = this.SupportParentProvider.GetSites().Where(IsMatch).ToList();
                list.ForEach(ModifySiteProperties);
                siteCollection.AddRange(list);
            }

            return siteCollection;
        }
    }
}