﻿using Core.Services;
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

        private static Dictionary<Type, Type> Laboratorio = new Dictionary<Type, Type> {
            [typeof(ILaboratory)] = typeof(LaboratoryImpl)
        };

        private static Dictionary<Type, Type> Projeto = new Dictionary<Type, Type> {
            [typeof(IProject)] = typeof(ProjectImpl)
        };

        private static Dictionary<Type, Type> Conexao = new Dictionary<Type, Type> {
            [typeof(IConection)] = typeof(ConexaoImpl)
        };

        private static Dictionary<Type, Type> Picture = new Dictionary<Type, Type> {
            [typeof(IPicture)] = typeof(PictureImpl)
        };

        internal static T GetInstanceOf<T>() {
            return Activator.CreateInstance<T>();
        }
    }
}