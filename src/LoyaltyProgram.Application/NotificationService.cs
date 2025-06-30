using LoyaltyProgram.Domain;


namespace LoyaltyProgram.Application
{
    public class NotificationService
    {
        public void NotifyNewRank(Client client, RankStatus newRank)
        {
            Console.WriteLine($"Client {client.FirstName} {client.LastName} has achieved a new rank: {newRank}");
        }

        public void NotifyPointsEarned(Client? client, int? pointsEarned)
        {
            Console.WriteLine($"Client {client?.FirstName} {client?.LastName} has earned {pointsEarned} points");
        }
    }
}