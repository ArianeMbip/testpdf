namespace ApiTestMongo.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class Eleves
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/eleves";
        public const string GetRecord = $"{Base}/eleves/{Id}";
        public const string Create = $"{Base}/eleves";
        public const string Delete = $"{Base}/eleves/{Id}";
        public const string Put = $"{Base}/eleves/{Id}";
        public const string CreateBatch = $"{Base}/eleves/batch";
    }

    public static class Users
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/users";
        public const string GetRecord = $"{Base}/users/{Id}";
        public const string Create = $"{Base}/users";
        public const string Delete = $"{Base}/users/{Id}";
        public const string Put = $"{Base}/users/{Id}";
        public const string CreateBatch = $"{Base}/users/batch";
        public const string AddRole = $"{Base}/users/{Id}/addRole";
        public const string RemoveRole = $"{Base}/users/{Id}/removeRole";
    }

    public static class RolePermissions
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/rolePermissions";
        public const string GetRecord = $"{Base}/rolePermissions/{Id}";
        public const string Create = $"{Base}/rolePermissions";
        public const string Delete = $"{Base}/rolePermissions/{Id}";
        public const string Put = $"{Base}/rolePermissions/{Id}";
        public const string CreateBatch = $"{Base}/rolePermissions/batch";
    }
}
