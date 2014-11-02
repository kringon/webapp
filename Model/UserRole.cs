namespace WebWarehouse.Model
{
    public enum UserRole
    {
        //Has all privileges
        Admin,

        //Can edit content, but not users
        Editor,

        //Can only use website / order
        Customer,

        Unknown
    }
}