using System.Collections.Specialized;
using System.Linq;
using Sitecore.Sites;

namespace Sitecore.Support.XA.Foundation.Multisite.Providers
{
  public class ServiceSiteProvider : Sitecore.XA.Foundation.Multisite.Providers.ServiceSiteProvider
  {
    public override void Initialize(string name, NameValueCollection config)
    {
      base.Initialize(name, config);
      _supportParentProviderName = config["inherits"] ?? string.Empty;
    }

    public override Site GetSite(string siteName)
    {
      if (SupportParentProvider != null)
      {
        Site site = SupportParentProvider.GetSite(siteName);
        if (site != null && IsMatch(site))
        {
          return site;
        }
      }
      return null;
    }

    public override SiteCollection GetSites()
    {
      SiteCollection siteCollection = new SiteCollection();
      if (SupportParentProvider != null)
      {
        siteCollection.AddRange(SupportParentProvider.GetSites().Where(IsMatch));
      }
      return siteCollection;
    }

    private string _supportParentProviderName;
    private SiteProvider _supportParentProvider;
    private SiteProvider SupportParentProvider
    {
      get
      {
        return _supportParentProvider ?? (_supportParentProvider = SiteManager.Providers[_supportParentProviderName]);
      }
    }
  }
}