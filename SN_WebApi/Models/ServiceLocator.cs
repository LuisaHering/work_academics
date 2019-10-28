using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models
{
    public class ServiceLocator
    {
        private static Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>
        {
            [typeof(IUser)] = typeof(UserService)
        };

        internal static T GetInstanceOf<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}