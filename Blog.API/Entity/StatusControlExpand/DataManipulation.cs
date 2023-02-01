using Blog.API.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace Blog.API.Entity.StatusControlExpand
{
    public static class DataManipulation
    {
        /// <summary>
        /// 修改指定属性的单条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="context">上下文</param>
        /// <param name="model">要修改的实体信息</param>
        /// <param name="modifyParams">指定修改的字段</param>
        public static void Modify<T>(this EFCoreContext context, T model, object modifyParams)
            where T : class
        {           
            // 循环 被修改的属性名 数组
            Type mps = modifyParams.GetType();
            PropertyInfo[] mpPropertys = mps.GetProperties();
            foreach (PropertyInfo item in mpPropertys)
            {
                ReflexHelper.SetModelValue(item.Name, ReflexHelper.GetModelValue(item.Name, modifyParams), model);
                context.Entry<T>(model).Property(item.Name).IsModified = true;
            }
            context.SaveChanges();
        }
    }
}
