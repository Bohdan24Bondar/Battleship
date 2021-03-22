using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public class PlayerMapSerializer : IDisposable
    {
        private string _pathToFolder;
        private string _pathToFile;
        private FileStream _destinationFile;
        private BinaryFormatter _fileOperator;
        private bool _isDisposed;

        public PlayerMapSerializer(string pathToFolder, string pathToFile)
        {
            _pathToFolder = pathToFolder;
            _pathToFile = pathToFile;
            _destinationFile = new FileStream(_pathToFile, FileMode.OpenOrCreate);
            _fileOperator = new BinaryFormatter();
            _isDisposed = false;
        }

        public static void CreateDirectory(string pathToFolder)
        {
            if (!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }
        }

        public void WriteMapsCondition<T>(T source)
        {
            _fileOperator.Serialize(_destinationFile, source);
        }

        public object ReadPlayerMap()
        {
            return _fileOperator.Deserialize(_destinationFile);
        }

        private void Clean()
        {
            _destinationFile.Dispose();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Clean();
                GC.SuppressFinalize(this);
            }

            _isDisposed = true;
        }

        ~PlayerMapSerializer()
        {
            Clean();
        }
    }
}
