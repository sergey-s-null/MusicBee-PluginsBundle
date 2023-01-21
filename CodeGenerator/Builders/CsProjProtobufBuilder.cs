using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Enums;
using Microsoft.Build.Evaluation;
using Root.Helpers;

namespace CodeGenerator.Builders
{
    public sealed class CsProjProtobufBuilder
    {
        private readonly Project _project;
        private ProtobufType _protobufType;
        private string _protoRoot = string.Empty;

        public CsProjProtobufBuilder(Project project)
        {
            _project = project;

            project.Save();
        }

        public CsProjProtobufBuilder RemoveAllProtobuf()
        {
            var currentProtobufItems = _project.Items
                .Where(x => x.ItemType == "Protobuf")
                .ToReadOnlyCollection();
            _project.RemoveItems(currentProtobufItems);

            return this;
        }

        public CsProjProtobufBuilder SetProtobufType(ProtobufType protobufType)
        {
            _protobufType = protobufType;
            return this;
        }

        public CsProjProtobufBuilder SetProtoRoot(string protoRoot)
        {
            _protoRoot = protoRoot;
            return this;
        }

        public CsProjProtobufBuilder AddProtobufItemGroup(IEnumerable<string> protoFilePaths)
        {
            var protobufGroup = _project.Xml.AddItemGroup();
            foreach (var protoFilePath in protoFilePaths)
            {
                protobufGroup.AddItem("Protobuf", protoFilePath, GetMetadata());
            }

            return this;
        }

        private IEnumerable<KeyValuePair<string, string>> GetMetadata()
        {
            var metadata = new List<KeyValuePair<string, string>>()
            {
                new("GrpcServices", _protobufType.ToString())
            };

            if (!string.IsNullOrEmpty(_protoRoot))
            {
                metadata.Add(new KeyValuePair<string, string>("ProtoRoot", _protoRoot));
            }
            
            return metadata;
        }

        public CsProjProtobufBuilder SaveProject()
        {
            _project.Save();
            return this;
        }
    }
}