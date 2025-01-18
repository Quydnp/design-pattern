using System;

namespace DesignPattern.Behavioral
{
    // Enum for request priority levels
    internal enum RequestPriority
    {
        Low,
        Medium,
        High
    }

    // Request class representing a support request
    class Request
    {
        public string Description { get; set; } = string.Empty;
        public RequestPriority Priority { get; set; }
    }

    // Abstract handler
    abstract class SupportHandler
    {
        protected SupportHandler? Next;

        public void SetNext(SupportHandler next)
        {
            Next = next;
        }

        public void HandleRequest(Request request)
        {
            bool handled = CanHandle(request);

            if (!handled && Next != null)
            {
                Next.HandleRequest(request); // Pass request to the next handler
            }
            else if (!handled)
            {
                Console.WriteLine($"Request '{request.Description}' with priority {request.Priority} was not handled.");
            }
        }

        protected abstract bool CanHandle(Request request);
    }

    // Concrete handler: Technician
    class Technician : SupportHandler
    {
        protected override bool CanHandle(Request request)
        {
            if (request.Priority == RequestPriority.Low)
            {
                Console.WriteLine($"Technician handled request: {request.Description}");
                return true;
            }
            return false;
        }
    }

    // Concrete handler: Support Engineer
    class SupportEngineer : SupportHandler
    {
        protected override bool CanHandle(Request request)
        {
            if (request.Priority == RequestPriority.Medium)
            {
                Console.WriteLine($"Support Engineer handled request: {request.Description}");
                return true;
            }
            return false;
        }
    }

    // Concrete handler: Team Lead
    class TeamLead : SupportHandler
    {
        protected override bool CanHandle(Request request)
        {
            if (request.Priority == RequestPriority.High)
            {
                Console.WriteLine($"Team Lead handled request: {request.Description}");
                return true;
            }
            return false;
        }
    }

    // Main application
    public class ChainOfResponsibility
    {
        /*public static void Main(string[] args)
        {
            // Create handlers
            var technician = new Technician();
            var supportEngineer = new SupportEngineer();
            var teamLead = new TeamLead();

            // Link handlers
            technician.SetNext(supportEngineer);
            supportEngineer.SetNext(teamLead);

            // Create and process requests
            var requests = new[]
            {
                new Request { Description = "Reset password", Priority = RequestPriority.Low },
                new Request { Description = "App not responding", Priority = RequestPriority.Medium },
                new Request { Description = "System crash", Priority = RequestPriority.High },
                new Request { Description = "Unknown issue", Priority = (RequestPriority)999 } // Invalid priority
            };

            foreach (var request in requests)
            {
                technician.HandleRequest(request);
            }
        }*/
    }
}
