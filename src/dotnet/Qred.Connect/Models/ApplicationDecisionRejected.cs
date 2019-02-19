namespace Qred.Connect
{
    public class ApplicationDecisionRejected : ApplicationDecision
    {
        public override ApplicationDecisionType Decision => ApplicationDecisionType.Denied;
        public string RejectReason { get; set; }
    }
}
