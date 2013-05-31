using ChineseCharacterTrainer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace ChineseCharacterTrainer.ServiceApp.Persistence
{
    public class ChineseTrainerContext : DbContext, IChineseTrainerContext
    {
        public ChineseTrainerContext(string databaseName)
            : base("data source=localhost;initial catalog=" + databaseName + ";integrated security=True;multipleactiveresultsets=True;App=EntityFramework")
        {
        }

        public ChineseTrainerContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new DictionaryEntryMapping());
            modelBuilder.Configurations.Add(new DictionaryMapping());
            modelBuilder.Configurations.Add(new TranslationMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new HighscoreMapping());
        }

        public List<Entity> GetAll(Type type)
        {
            var dbSet = Set(type);
            dbSet.Load();

            var listType = typeof(List<>);
            Type[] typeArgs = { type };
            var listTypeGenericRuntime = listType.MakeGenericType(typeArgs);
            var enumerable = Activator.CreateInstance(listTypeGenericRuntime, dbSet.Local) as IEnumerable;
            
            return (from object item in enumerable select item as Entity).ToList();
        }

        public void Add(Type type, Entity entity)
        {
            var navigationPropertyInfos = GetNavigationPropertyInfos(entity);
            foreach (var navigationPropertyInfo in navigationPropertyInfos)
            {
                var navigationProperty = navigationPropertyInfo.GetValue(entity) as Entity;

                var set = Set(navigationPropertyInfo.PropertyType);
                set.Load();

                var existingEntity = set.Find(navigationProperty.Id);
                if (existingEntity != null)
                {
                    navigationPropertyInfo.SetValue(entity, null, null);
                }
            }

            Set(type).Add(entity);
        }

        private static IEnumerable<PropertyInfo> GetNavigationPropertyInfos(Entity entity)
        {
            var type = entity.GetType();
            var allProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var navigationProperties = allProperties.Where(p => p.PropertyType.IsSubclassOf(typeof (Entity))).ToList();
            return navigationProperties;
        }
    }
}
