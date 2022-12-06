using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Blog.API.Dynamic.Api.Core.Helper
{
    public static class VerifyNullOrEmpty
    {
        public static bool IsNullOrEmpty(this IList<IActionConstraintMetadata> iListActionConstraintMetadata)
        {
            return iListActionConstraintMetadata.Count <= 0 || !iListActionConstraintMetadata.Any();
        }
        public static bool IsNullOrEmpty(this IList<Object> iListObject)
        {
            return iListObject.Count <= 0 || !iListObject.Any();
        }
        public static bool IsNullOrEmpty(this IList<SelectorModel> iListSelectorModel)
        {
            return iListSelectorModel.Count <= 0 || !iListSelectorModel.Any();
        }
    }
}
