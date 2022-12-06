

using Blog.API.Dynamic.Api.Core.Attributes;
using Blog.API.Dynamic.Api.Core.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace Blog.API.Dynamic.Api.Core
{
    internal class ApiConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (typeof(IService).IsAssignableFrom(controller.ControllerType))
                {
                    ConfigureApplicationService(controller);
                }
            }
        }

        private void ConfigureApplicationService(ControllerModel controller)
        {
            ConfigureApiExplorer(controller);
            ConfigureSelector(controller);
            ConfigureParameters(controller);
        }
        /// <summary>
        /// 让对应动态生成的API和下面的方法能够被发现（Swagger），硬被发现
        /// </summary>
        /// <param name="controller"></param>
        private void ConfigureApiExplorer(ControllerModel controller)
        {
            if (!controller.ApiExplorer.IsVisible.HasValue)
            {
                controller.ApiExplorer.IsVisible = true;
            }

            foreach (var action in controller.Actions)
            {
                if (!action.ApiExplorer.IsVisible.HasValue)
                {
                    action.ApiExplorer.IsVisible = true;
                }
            }

            foreach (var action in controller.Actions)
            {
                if (!CheckNoMapMethod(action))
                    ConfigureApiExplorer(action);
            }
        }

        private void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible == null)
            {
                action.ApiExplorer.IsVisible = true;
            }
        }
        /// <summary>
        /// //不映射指定的方法
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool CheckNoMapMethod(ActionModel action)
        {
            bool isExist = false;
            var noMapMethod = AttributeHelper.GetSingleAttributeOrDefault<NonDynamicActionAttribute>(action.ActionMethod);

            if (noMapMethod != null)
            {
                action.ApiExplorer.IsVisible = false;//对应的Api不映射
                isExist = true;
            }

            return isExist;
        }
        /// <summary>
        /// 路由配置
        /// </summary>
        /// <param name="controller"></param>
        private void ConfigureSelector(ControllerModel controller)
        {
            RemoveEmptySelectors(controller.Selectors);

            if (controller.Selectors.Any(temp => temp.AttributeRouteModel != null))
            {
                return;
            }

            foreach (var action in controller.Actions)
            {
                ConfigureSelector(action);
            }
        }
        //处理action
        private void ConfigureSelector(ActionModel action)
        {
            RemoveEmptySelectors(action.Selectors);

            if (action.Selectors.IsNullOrEmpty() || action.Selectors.Any(a => a.ActionConstraints.IsNullOrEmpty()))
            {
                if (!CheckNoMapMethod(action))
                    AddApplicationServiceSelector(action);
            }
            else
            {
                NormalizeSelectorRoutes(action);
            }
        }
        /// <summary>
        /// 参数配置
        /// </summary>
        /// <remarks>https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Mvc/Volo/Abp/AspNetCore/Mvc/Conventions/AbpServiceConvention.cs#L67</remarks>
        /// <param name="controller"></param>
        private void ConfigureParameters(ControllerModel controller)
        {
            foreach (var action in controller.Actions)
            {
                if (!CheckNoMapMethod(action)) {
                    foreach (var parameter in action.Parameters)
                    {
                        if (parameter.BindingInfo != null)
                        {
                            continue;
                        }

                        if (parameter.ParameterType.IsClass &&
                            parameter.ParameterType != typeof(string) &&
                            parameter.ParameterType != typeof(IFormFile))
                        {
                            var httpMethods = action.Selectors.SelectMany(temp => temp.ActionConstraints).OfType<HttpMethodActionConstraint>().SelectMany(temp => temp.HttpMethods).ToList();
                            if (httpMethods.Contains("GET") ||
                                httpMethods.Contains("DELETE") ||
                                httpMethods.Contains("TRACE") ||
                                httpMethods.Contains("HEAD"))
                            {
                                continue;
                            }

                            parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                        }
                    }
                }                    
            }
        }

        private void NormalizeSelectorRoutes(ActionModel action)
        {
            foreach (var selector in action.Selectors)
            {
                //没有路由，补上
                selector.AttributeRouteModel ??= new AttributeRouteModel(new RouteAttribute(CalculateRouteTemplate(action)));
                //没有约束，补上
                if (selector.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() == null)
                {
                    selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
                }
            }
        }

        private void AddApplicationServiceSelector(ActionModel action)
        {
            var selector = new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(CalculateRouteTemplate(action)))
            };
            selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));

            action.Selectors.Add(selector);
        }

        private string CalculateRouteTemplate(ActionModel action)
        {
            var routeTemplate = new StringBuilder();
            routeTemplate.Append("api");

            // 控制器名称部分
            var controllerName = action.Controller.ControllerName;
            if (controllerName.EndsWith("ApplicationService"))
            {
                controllerName = controllerName[..^"ApplicationService".Length];
            }
            else if (controllerName.EndsWith("AppService"))
            {
                controllerName = controllerName[..^"AppService".Length];
            }
            controllerName += "s";
            routeTemplate.Append($"/{controllerName}");

            // Action 名称部分
            var actionName = action.ActionName;
            if (actionName.EndsWith("Async"))
            {
                actionName = actionName[..^"Async".Length];
            }
            var trimPrefixes = new[]
            {
                "GetAll","GetList","Get",
                "Post","Create","Add","Insert",
                "Put","Update",
                "Delete","Remove",
                "Patch"
            };
            foreach (var trimPrefix in trimPrefixes)
            {
                if (actionName.StartsWith(trimPrefix))
                {
                    actionName = actionName[trimPrefix.Length..];
                    break;
                }
            }
            if (!string.IsNullOrEmpty(actionName))
            {
                routeTemplate.Append($"/{actionName}");
            }

            // id 部分
            if (action.Parameters.Any(temp => temp.ParameterName == "id"))
            {
                routeTemplate.Append("/{id}");
            }
            return routeTemplate.ToString();
        }

        private string GetHttpMethod(ActionModel action)
        {
            var actionName = action.ActionName;
            if (actionName.StartsWith("Get") || actionName.StartsWith("get"))
            {
                return "GET";
            }

            if (actionName.StartsWith("Put") || actionName.StartsWith("Update") || actionName.StartsWith("put") || actionName.StartsWith("update"))
            {
                return "PUT";
            }

            if (actionName.StartsWith("Delete") || actionName.StartsWith("Del") || actionName.StartsWith("Remove") || actionName.StartsWith("delete") || actionName.StartsWith("del") || actionName.StartsWith("remove"))
            {
                return "DELETE";
            }

            if (actionName.StartsWith("Patch") || actionName.StartsWith("patch"))
            {
                return "PATCH";
            }

            return "POST";
        }
        /// <summary>
        /// 删除生成API后.NET框架根据API生成的空白信息
        /// </summary>
        /// <remarks>https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.AspNetCore.Mvc/Volo/Abp/AspNetCore/Mvc/Conventions/AbpServiceConvention.cs#L170</remarks>
        /// <param name="selectors"></param>
        private void RemoveEmptySelectors(IList<SelectorModel> selectors)
        {
            selectors
            .Where(IsEmptySelector)
            .ToList()
            .ForEach(s => selectors.Remove(s));
        }
        private bool IsEmptySelector(SelectorModel selector)
        {
            return selector.AttributeRouteModel == null
                   && selector.ActionConstraints.IsNullOrEmpty()
                   && selector.EndpointMetadata.IsNullOrEmpty();
        }

    }
}
