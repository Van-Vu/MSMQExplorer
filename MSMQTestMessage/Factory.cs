using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQTestMessage
{
    using System.Reflection;

    public class Factory
    {
        public List<Type> GetMessageList(string sAssemblyFileName)
        {
            try
            {
                var assem = Assembly.LoadFrom(sAssemblyFileName);
                List<Type> types = assem.GetTypes().Where(t => t.IsClass && !t.IsAbstract).OrderBy(x => x.FullName).ToList();

                //Type[] types = assem.GetTypes();
                return types;
            }
            catch (Exception ex)
            {
                string tst = ex.Message;
                throw;
            }
        }

        public List<string> GetMessageProperties(Type type)
        {
            PropertyInfo[] list = type.GetProperties();
            //object ClassObj = Activator.CreateInstance(type);
            return (from item in list select item.Name).ToList();
        }
    }
}
