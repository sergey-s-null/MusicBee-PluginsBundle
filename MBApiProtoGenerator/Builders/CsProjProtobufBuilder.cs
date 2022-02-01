using System.Collections.Generic;
using System.Linq;
using MBApiProtoGenerator.Enums;
using Microsoft.Build.Evaluation;
using Root.Helpers;

namespace MBApiProtoGenerator.Builders
{
    public class CsProjProtobufBuilder
    {
        private readonly Project _project;
        private ProtobufType _protobufType;
        
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
        
        public CsProjProtobufBuilder AddProtobufItemGroup(IEnumerable<string> protoFilePaths)
        {
            var protobufGroup = _project.Xml.AddItemGroup();
            foreach (var protoFilePath in protoFilePaths)
            {
                protobufGroup.AddItem("Protobuf", protoFilePath, new[]
                {
                    new KeyValuePair<string, string>("GrpcServices", _protobufType.ToString())
                });
            }

            return this;
        }

        public CsProjProtobufBuilder SaveProject()
        {
            _project.Save();
            return this;
        }
    }
}