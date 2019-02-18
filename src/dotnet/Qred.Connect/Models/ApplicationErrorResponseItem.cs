using System;
using System.Collections.Generic;

namespace Qred.Connect
{
    public class ApplicationErrorResponse
    {
        public List<ApplicationErrorResponseItem> Errors { get; set; }
    }

    public class ApplicationErrorResponseItem
    {
        public string Property { get; set; }
        public string Description { get; set; }

    }
}
