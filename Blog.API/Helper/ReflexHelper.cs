namespace Blog.API.Helper
{
    /// <summary>
    /// 反射工具
    /// </summary>
    public static class ReflexHelper
    {
        #region 对象相关 
        /// <summary>
        /// 取对象属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetModelValue(string FieldName, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object o = Ts.GetProperty(FieldName).GetValue(obj, null);
                if (null == o)
                    return null;
                string Value = Convert.ToString(o);
                if (string.IsNullOrEmpty(Value))
                    return null;
                return Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// 设置对象属性值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="Value"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool SetModelValue(string FieldName, string Value, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object v = Convert.ChangeType(Value, Ts.GetProperty(FieldName).PropertyType);
                Ts.GetProperty(FieldName).SetValue(obj, v, null);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
        #endregion
    }
}
