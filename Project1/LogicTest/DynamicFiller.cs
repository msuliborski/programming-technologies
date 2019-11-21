using Data;
using System.IO;
using System.Collections.Generic;

namespace Tests {

    public class DynamicFiller : IFiller {
        public void Fill(DataRepository dataRepository) {

            using(Stream stream = File.Open("../../../Catalogs.ser", FileMode.Open)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                dataRepository.SetCatalogs((List<Catalog>)binaryFormatter.Deserialize(stream));
            }

            using(Stream stream = File.Open("../../../Readers.ser", FileMode.Open)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                dataRepository.SetReaders((List<Reader>)binaryFormatter.Deserialize(stream));
            }
        }
    }
}