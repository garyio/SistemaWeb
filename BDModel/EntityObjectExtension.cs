using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace BDModel
{
    public static class EntityObjectExtension
    {
        /// <summary>
        /// Bitmask joining EntityState Added and Detached
        /// </summary>
        private static int _AddedOrDetached = (int)EntityState.Added + (int)EntityState.Detached;
        private static string _Id = "Id";

        /// <summary>
        /// Checks if the current EntityObject can call its EntityObject.Load() Method
        /// </summary>
        public static bool IsLoadable(this EntityObject entity)
        {
            return ((int)entity.EntityState & _AddedOrDetached) == 0;
        }

        /// <summary>
        /// Extracts the contained Entity from an entityReference
        /// </summary>
        public static T Load<T>(this EntityObject entity, EntityReference<T> entityReference) where T : EntityObject
        {
            if (entity.IsLoadable() && !entityReference.IsLoaded) try { entityReference.Load(); }
                catch { }
            return entityReference.Value;
        }

        /// <summary>
        /// Loads Explicitly all Entities contained in the EntityCollection
        /// </summary>
        public static EntityCollection<T> Load<T>(this EntityObject entity, EntityCollection<T> entityCollection) where T : EntityObject
        {
            if (entity.IsLoadable() && !entityCollection.IsLoaded) entityCollection.Load();
            return entityCollection;
        }

        /// <summary>
        /// Returns the ID (primary key) of the Entity
        /// </summary>
        public static int? GetId(this EntityObject entity)
        {
            foreach (var item in entity.EntityKey.EntityKeyValues) if (item.Key == _Id) return item.Value is int ? (int)item.Value : (int?)null;
            return null;
        }

        /// <summary>
        /// Gets a loglist of all users that commited modifications to the entity
        /// </summary>
        //public static List<UsuarioLog> GetUsuarioLogs(this EntityObject entity)
        //{
        //    int id = entity.GetId() ?? -1;
        //    if (id == -1) return new List<UsuarioLog>();

        //    Tabla tabla = Tabla.Get(entity.GetType());
        //    using (FletesEntities model = new FletesEntities(FletesEntities.DefaultConnectionString))
        //    {
        //        return model.UsuarioLog.Where<UsuarioLog>(usuarioLog => (usuarioLog.Tabla.Id == tabla.Id) && (usuarioLog.ObjectoId == id)).ToList();
        //    }
        //}

        /// <summary>
        /// Gets the first user that created the entity
        /// </summary>
        //public static Usuario GetUsuarioFirst(this EntityObject entity)
        //{
        //    int id = entity.GetId() ?? -1;
        //    if (id == -1) return null;

        //    Tabla tabla = Tabla.Get(entity.GetType());
        //    using (FletesEntities model = new FletesEntities(FletesEntities.DefaultConnectionString))
        //    {
        //        var log = model.UsuarioLog.FirstOrDefault<UsuarioLog>(usuarioLog => (usuarioLog.Tabla.Id == tabla.Id) && (usuarioLog.ObjectoId == id));
        //        return log == null ? null : log.Load(log.UsuarioReference);
        //    }
        //}

        /// <summary>
        /// Gets the last user that modified the entity
        /// </summary>
        //public static Usuario GetUsuarioLast(this EntityObject entity)
        //{
        //    int id = entity.GetId() ?? -1;
        //    if (id == -1) return null;

        //    Tabla tabla = Tabla.Get(entity.GetType());
        //    using (FletesEntities model = new FletesEntities(FletesEntities.DefaultConnectionString))
        //    {
        //        var log = model.UsuarioLog.LastOrDefault<UsuarioLog>(usuarioLog => (usuarioLog.Tabla.Id == tabla.Id) && (usuarioLog.ObjectoId == id));
        //        return log == null ? null : log.Load(log.UsuarioReference);
        //    }
        //}

    }
}
