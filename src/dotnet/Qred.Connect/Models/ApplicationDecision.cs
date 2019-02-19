using System;

namespace Qred.Connect
{
    public abstract class ApplicationDecision
    { 
        public abstract ApplicationDecisionType Decision { get; }
        public Uri UserPage { get; set; }
    }
}
