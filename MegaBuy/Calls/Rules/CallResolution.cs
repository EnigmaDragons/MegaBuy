
namespace MegaBuy.Calls.Rules
{
    public enum CallResolution
    {
        Any,
        CallerHangUp,
        ReferToInfo,
        ReferToTroubleshooting,
        ReferToReturns,
        ReferToCareers,
        ReferToOrders,
        EscalateCall,
        ReferToLegal,
        ReferToAccounting,
        ReferToRecommendations,
        ReferToFeedback,
        ReferToGeneralist,
        ApproveReturn,
        ApproveReplacement,
        Reject,
    }
}
