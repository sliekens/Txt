namespace SLANG
{
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinaryTextReaderTests
    {
        private const string OneParagraph = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis facilisis fringilla leo, ut consequat magna lobortis sed. In et rhoncus neque, in mattis eros. Aenean viverra lectus commodo sapien eleifend, non gravida urna ultrices. Mauris malesuada mauris eget viverra dictum. Nulla urna est, commodo a neque ut, commodo imperdiet erat. Mauris rhoncus viverra nisi, sed ornare est consequat eget. Aenean purus orci, varius eget ante at, volutpat cursus nisi. In non nulla rutrum arcu lobortis auctor.";
        private const string FiveParagraphs = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis facilisis fringilla leo, ut consequat magna lobortis sed. In et rhoncus neque, in mattis eros. Aenean viverra lectus commodo sapien eleifend, non gravida urna ultrices. Mauris malesuada mauris eget viverra dictum. Nulla urna est, commodo a neque ut, commodo imperdiet erat. Mauris rhoncus viverra nisi, sed ornare est consequat eget. Aenean purus orci, varius eget ante at, volutpat cursus nisi. In non nulla rutrum arcu lobortis auctor.

Integer sapien risus, pellentesque eu auctor sed, tincidunt sed nisi. Donec ac quam vulputate, volutpat est at, laoreet nunc. Etiam lacinia tellus diam, sit amet condimentum est placerat sed. Curabitur interdum ipsum id mollis aliquam. Etiam et feugiat sapien, a facilisis urna. Vestibulum rutrum, ante vel mattis viverra, odio eros convallis arcu, vel viverra dui nisl vitae urna. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean imperdiet, velit non eleifend vestibulum, lacus lacus sollicitudin leo, eu sollicitudin ex est id justo. Etiam luctus venenatis dui, nec luctus purus ultricies sit amet. Praesent ornare massa id nisl tincidunt, nec commodo turpis tempus. Maecenas vel ornare risus, nec ultrices ligula. Suspendisse suscipit nunc in porttitor fermentum. Aliquam volutpat ligula ac imperdiet interdum. Vivamus congue ultrices odio, sed consequat orci ullamcorper vitae.

Morbi enim leo, cursus in odio non, pharetra imperdiet arcu. Donec ut libero eget ex scelerisque placerat ut et velit. Cras ut eleifend massa. In molestie dignissim ex sed semper. Quisque interdum porttitor leo. Suspendisse id diam quis nisi volutpat mattis sed vel magna. Aenean in condimentum turpis.

Pellentesque suscipit, metus id luctus mattis, diam tellus rutrum turpis, at venenatis mi libero vitae nisi. Nam mollis imperdiet risus quis porttitor. Aliquam ac pulvinar odio, et semper enim. Donec massa nisl, commodo quis ullamcorper sed, pulvinar eu lorem. Ut velit felis, pharetra eu ante a, posuere lobortis nibh. Morbi dictum et metus ut imperdiet. Etiam felis ex, consequat eget porta id, posuere a massa. Etiam scelerisque enim sit amet blandit semper. Duis iaculis sed mi vitae commodo. Sed ut nunc vel nulla commodo consectetur. Cras vel tortor eleifend, volutpat libero in, pellentesque sem. Fusce sodales tincidunt odio quis molestie. Suspendisse risus dolor, volutpat eu congue sit amet, cursus quis augue. Donec vulputate viverra magna, at finibus metus rutrum et. Aliquam vestibulum urna felis, nec fermentum quam pulvinar sed. Suspendisse tempor elementum metus, non laoreet mi volutpat quis.

Mauris porta arcu eu consectetur porttitor. Sed leo nisi, ultrices vel fringilla id, convallis quis lorem. Morbi facilisis tortor erat. Nulla bibendum euismod sollicitudin. Donec elit urna, auctor nec interdum ut, auctor id velit. Vivamus rutrum ut risus id sollicitudin. Duis tempus metus sed diam aliquet, eget laoreet metus convallis. Aliquam id sem ut neque congue faucibus quis eget neque. Mauris molestie orci ipsum, id pharetra odio auctor nec. Vestibulum posuere nisi eu justo blandit, ac placerat lectus semper. Nulla sed pellentesque dui, sit amet laoreet risus. Etiam nunc nulla, convallis eget tortor feugiat, ornare faucibus nisl. Nullam leo diam, dapibus id massa id, pulvinar convallis mauris.";

        [TestMethod]
        public void Read_WithOneParagraph()
        {
            var bytes = Encoding.UTF8.GetBytes(OneParagraph);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var buffer = new char[OneParagraph.Length];
                var read = textReader.Read(buffer, 0, buffer.Length);
                var output = new string(buffer, 0, read);
                Assert.AreEqual(OneParagraph, output);
            }
        }

        [TestMethod]
        public void Read_WithEmptyString()
        {
            var bytes = Encoding.UTF8.GetBytes(string.Empty);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var buffer = new char[16];
                var read = textReader.Read(buffer, 0, buffer.Length);
                var output = new string(buffer, 0, read);
                Assert.AreEqual(string.Empty, output);
            }
        }

        [TestMethod]
        public void ReadBlock_WithOneParagraph()
        {
            var bytes = Encoding.UTF8.GetBytes(OneParagraph);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var buffer = new char[OneParagraph.Length];
                var read = textReader.ReadBlock(buffer, 0, buffer.Length);
                var output = new string(buffer, 0, read);
                Assert.AreEqual(OneParagraph, output);
            }
        }

        [TestMethod]
        public void ReadBlock_WithEmptyString()
        {
            var bytes = Encoding.UTF8.GetBytes(string.Empty);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var buffer = new char[16];
                var read = textReader.ReadBlock(buffer, 0, buffer.Length);
                var output = new string(buffer, 0, read);
                Assert.AreEqual(string.Empty, output);
            }
        }

        [TestMethod]
        public void ReadToEnd_WithFiveParagraphs()
        {
            var bytes = Encoding.UTF8.GetBytes(FiveParagraphs);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var output = textReader.ReadToEnd();
                Assert.AreEqual(FiveParagraphs, output);
            }
        }

        [TestMethod]
        public void ReadToEnd_WithEmptyString()
        {
            var bytes = Encoding.UTF8.GetBytes(string.Empty);
            using (var ms = new MemoryStream(bytes))
            using (var binaryReader = new BinaryReader(ms))
            using (var textReader = new BinaryTextReader(binaryReader))
            {
                var output = textReader.ReadToEnd();
                Assert.AreEqual(string.Empty, output);
            }
        }
    }
}
