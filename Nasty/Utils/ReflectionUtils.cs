using System.Collections.Generic;
using System.Reflection;
using System;
namespace Nasty.Utils
{
    /**
     * Some useful reflection helpers
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class ReflectionUtils {

	    public static ICollection<PropertyInfo> GetAllInitProperties(Type clazz, Type attr) {
            var fields = new List<PropertyInfo>();
		    foreach (var f in clazz.GetProperties()) {
			    if(f.GetCustomAttributes(attr, true).Length > 0)
				    fields.Add(f);
		    }
            /*
		    if (clazz.BaseType != null) {
			    fields.AddRange(getAllInitProperties(clazz.BaseType, attr));
		    }*/
		    return fields;
	    }

        public static PropertyInfo FindField(Type clazz, string name)
        {
		    if(clazz == null) return null;
			var property = clazz.GetProperty(name);
            return property ?? FindField(clazz.BaseType, name);
        }
	
	    public static void CopyInitProperties(object src, object target, Type attr) {
		    var srcClass = src.GetType();
		
		    foreach(var f in GetAllInitProperties(target.GetType(), attr)) {
			    if(f.GetSetMethod().IsStatic) continue;
				var srcField = FindField(srcClass, f.Name);
				if(srcField != null) {
					f.SetValue(target, srcField.GetValue(src, new object[] {}), new object[] {});
				}
		    }
	    }
    }
}