using System;
using System.Collections.Generic;
using MegaBuy.Calls.Rules;
using MonoDragons.Core.Common;

namespace MegaBuy.ReturnCalls.BetweenCalls
{
    public static class CallSummaries
    {
        public static List<string> HangUpSummaries = new List<string>
        {
            "Do you like making customers mad?",
            "You're slacking off arn't you?",
            "I just recieved a horrible customer review about you.",
            "DO YOUR JOB!",
            "Are you sleeping!",
        };

        public static List<string> WronglyRejectedSummaries = new List<string>
        {
            "If you keep at it, I am going to be forced to reject your paycheck!",
            "You can't reject people just because you don't like how they look!",
            "Does it make you feel powerful when you wrongly excercise your power of refusal!",
        };

        public static List<string> RejectedSummaries = new List<string>
        {
            "That customer wrongly thought he could trick our top-notch customer support.",
            "That last customer was an idiot",
            "Congratulations! You just saved your company money."
        };

        public static List<string> WronglyApprovedReplacement = new List<string>
        {
            "Just a few more mistakes before we replace you!",
            "Did you replace your brain with rock, you idiot!"
        };

        public static List<string> ApprovedReplacement = new List<string>
        {
            "Excellent we get to keep their sweet credits.",
            "Excellent! Just make sure not to tell them that their replacement will be defective.",
        };

        public static List<string> WronglyApprovedReturns = new List<string>
        {
            "Do you like losing money?",
            "You can't give free shit!",
            "What if i gave away your stuff!",
        };

        public static List<string> ApprovedReturns = new List<string>
        {
            "At least you can do your job even if the people who make these defective things can't!",
            "You might be better then your idiot co-workers.",
            "The customer will definitely buy more stuff from us.",
        };

        public static string GetSummary(CallResolution resolution, bool technicalMistakeWasMade)
        {
            if (CallResolution.CallerHangUp == resolution)
                return HangUpSummaries.Random();
            if (CallResolution.Reject == resolution && technicalMistakeWasMade)
                return WronglyRejectedSummaries.Random();
            if (CallResolution.Reject == resolution && !technicalMistakeWasMade)
                return RejectedSummaries.Random();
            if (CallResolution.ApproveReplacement == resolution && technicalMistakeWasMade)
                return WronglyApprovedReplacement.Random();
            if (CallResolution.ApproveReplacement == resolution && !technicalMistakeWasMade)
                return ApprovedReplacement.Random();
            if (CallResolution.ApproveReturn == resolution && technicalMistakeWasMade)
                return WronglyApprovedReturns.Random();
            if (CallResolution.ApproveReturn == resolution && !technicalMistakeWasMade)
                return ApprovedReturns.Random();
            throw new ArgumentException("Invalid Call Resolution");
        }
    }
}
