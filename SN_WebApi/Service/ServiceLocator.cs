using Core.Services;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Service {
    public class ServiceLocator {
        private static Dictionary<Type, Type> Usuario = new Dictionary<Type, Type> {
            [typeof(IUsers)] = typeof(UsersImpl)
        };

        internal static T GetInstanceOf<T>() {
            return Activator.CreateInstance<T>();
        }
    }
}