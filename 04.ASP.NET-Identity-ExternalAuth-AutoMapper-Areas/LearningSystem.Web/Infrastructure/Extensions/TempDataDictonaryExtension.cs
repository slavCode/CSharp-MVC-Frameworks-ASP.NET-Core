namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public static class TempDataDictonaryExtension
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
           => tempData[WebGlobulConstants.TempDataSuccessMessageKey] = message;

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
           => tempData[WebGlobulConstants.TempDataErrorMessageKey] = message;
    }
}
