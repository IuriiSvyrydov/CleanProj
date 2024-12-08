

namespace CleanProj.API.Helpers;

public static class MessageHelper
{
    public static string GeneralValidationErrorMessage =>"One or more validation errors occurred";
    public static string GetApiSuccessCreatedMessage(string entityName) => $"{entityName} successfully created";
    public static string GetApiSuccessUpdatedMessage(string entityName) => $"{entityName} successfully updated";
    public static string GetApiSuccessDeletedMessage(string entityName) => $"{entityName} successfully deleted";
}