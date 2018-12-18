using System.Linq;
using Sitecore.Sites;

namespace Sitecore.Support.XA.Foundation.Multisite.Providers
{
  public class BackendSiteProvider : Sitecore.XA.Foundation.Multisite.Providers.BackendSiteProvider
  {
    public override Site GetSite(string siteName)
    {
      if (ParentProvider != null)
      {
        Site site = ParentProvider.GetSite(siteName);
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
      if (ParentProvider != null)
      {
        siteCollection.AddRange(ParentProvider.GetSites().Where(IsMatch));
      }
      return siteCollection;
    }
  }
}