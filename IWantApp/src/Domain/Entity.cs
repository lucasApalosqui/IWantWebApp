using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace IWantApp.Domain;

public abstract class Entity : Notifiable<Notification>
{
    
    public Entity()
    {
        Id = Guid.NewGuid();
    }


    public Guid Id { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreateOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }
}
