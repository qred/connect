using System.Collections.Generic;

namespace Qred.Connect.Admin
{
  /// <summary>
  /// 
  /// </summary>
  public partial class ApplicationsPage 
    { 
        /// <summary>
        /// Gets or Sets Total
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or Sets PerPage
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or Sets Pages
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        public List<ApplicationPageItem> Items { get; set; }
    }
}
