using System;
using FluentNHibernate.Infrastructure;

namespace FluentNHibernate.MappingModel.Output
{
    public class XmlWriterContainer : Container
    {
        public XmlWriterContainer()
        {
            RegisterWriter<HibernateMapping>(c =>
                new XmlHibernateMappingWriter(c.Resolve<IXmlWriter<ClassMapping>>(), c.Resolve<IXmlWriter<ImportMapping>>()));

            RegisterWriter<ClassMapping>(c =>
                new XmlClassWriter(c.Resolve<IXmlWriter<PropertyMapping>>(), c.Resolve<IXmlWriter<DiscriminatorMapping>>(), c.Resolve<IXmlWriter<ISubclassMapping>>(), c.Resolve<IXmlWriter<DynamicComponentMapping>>()));

            RegisterWriter<ImportMapping>(c =>
                new XmlImportWriter());

            RegisterWriter<PropertyMapping>(c =>
                new XmlPropertyWriter(c.Resolve<IXmlWriter<ColumnMapping>>()));

            RegisterWriter<DynamicComponentMapping>(c =>
                new XmlDynamicComponentWriter(c.Resolve<IXmlWriter<PropertyMapping>>(), c.Resolve<IXmlWriter<ParentMapping>>()));

            RegisterWriter<ColumnMapping>(c =>
                new XmlColumnWriter());

            RegisterWriter<DiscriminatorMapping>(c =>
                new XmlDiscriminatorWriter());

            RegisterWriter<KeyMapping>(c =>
                new XmlKeyWriter());

            RegisterWriter<ParentMapping>(c =>
                new XmlParentWriter());

            // subclasses
            RegisterWriter<ISubclassMapping>(c =>
                new XmlInheritanceWriter(c.Resolve<IXmlWriter<SubclassMapping>>(), c.Resolve<IXmlWriter<JoinedSubclassMapping>>()));

            RegisterWriter<SubclassMapping>(c =>
                new XmlSubclassWriter(c.Resolve<IXmlWriter<PropertyMapping>>(), c.Resolve<IXmlWriter<DynamicComponentMapping>>()));

            RegisterWriter<JoinedSubclassMapping>(c =>
                new XmlJoinedSubclassWriter(c.Resolve<IXmlWriter<PropertyMapping>>(), c.Resolve<IXmlWriter<KeyMapping>>(), c.Resolve<IXmlWriter<DynamicComponentMapping>>()));
        }

        private void RegisterWriter<T>(Func<Container, object> instantiate)
        {
            Register<IXmlWriter<T>>(instantiate);
        }
    }
}