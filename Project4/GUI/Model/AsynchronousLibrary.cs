using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace GUI.Model {
    public class AsynchronousLibrary {

        private static AsynchronousLibrary instance;
        private Library library;

        private AsynchronousLibrary() {
            library = new Library();
        }

        public static AsynchronousLibrary Instance {
            get {
                if (instance == null) {
                    instance = new AsynchronousLibrary();
                }
                return instance;
            }
        }
    }
}
