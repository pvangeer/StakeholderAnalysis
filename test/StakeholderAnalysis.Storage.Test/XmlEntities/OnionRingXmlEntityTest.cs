using NUnit.Framework;
using System;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using StakeholderAnalysis.Storage.XmlEntities;

namespace StakeholderAnalysis.Storage.Test.XmlEntities
{
    public class OnionRingXmlEntityTest
    {
        [Test]
        public void SerializationTest()
        {
            var backgroundColor = Colors.Aquamarine;
            var percentage = .3;
            var strokeThickness = 2.6;
            var strokeColor = Colors.DimGray;
            var entity = new OnionRingXmlEntity
            {
                Percentage = percentage,
                BackgroundColor = backgroundColor.ToHexString(),
                StrokeColor = strokeColor.ToHexString(),
                StrokeThickness = strokeThickness
            };

            Stream stream = new MemoryStream();

            var serializer = new XmlSerializer(typeof(OnionRingXmlEntity));
            serializer.Serialize(stream, entity);
            serializer.Serialize(Console.Out, entity);

            // TODO: Add correct asserts (by deserializing?) This is not a test.
            Assert.AreNotEqual(0, stream.Length);
        }
    }
}
