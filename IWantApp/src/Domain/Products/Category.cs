using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; set; }
    public bool Active { get; set; } 

    public Category(string name, string editedBy, string createBy)
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(name, "Name")
            .IsGreaterOrEqualsThan(name, 3, "Name")
            .IsNotNullOrEmpty(editedBy, "EditedBy")
            .IsNotNullOrEmpty(createBy, "CreateBy");
        AddNotifications(contract);


        Name = name;
        Active = true;
        EditedBy = editedBy;
        CreateBy = createBy;
        CreateOn = DateTime.Now;
        EditedOn = DateTime.Now;
    }

}

