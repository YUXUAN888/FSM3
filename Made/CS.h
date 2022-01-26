#include <grpcpp/grpcpp.h>

#include "src/proto/grpc/reflection/v1alpha/reflection.grpc.pb.h"

namespace grpc {

    class ProtoServerReflection final
        : public reflection::v1alpha::ServerReflection::Service {
    public:
        ProtoServerReflection();

        // Add the full names of registered services
        void SetServiceList(const std::vector<std::string>* services);

        // implementation of ServerReflectionInfo(stream ServerReflectionRequest) rpc
        // in ServerReflection service
        Status ServerReflectionInfo(
            ServerContext* context,
            ServerReaderWriter<reflection::v1alpha::ServerReflectionResponse,
            reflection::v1alpha::ServerReflectionRequest>* stream)
            override;

    private:
        Status ListService(ServerContext* context,
            reflection::v1alpha::ListServiceResponse* response);

        Status GetFileByName(ServerContext* context, const std::string& file_name,
            reflection::v1alpha::ServerReflectionResponse* response);

        Status GetFileContainingSymbol(
            ServerContext* context, const std::string& symbol,
            reflection::v1alpha::ServerReflectionResponse* response);

        Status GetFileContainingExtension(
            ServerContext* context,
            const reflection::v1alpha::ExtensionRequest* request,
            reflection::v1alpha::ServerReflectionResponse* response);

        Status GetAllExtensionNumbers(
            ServerContext* context, const std::string& type,
            reflection::v1alpha::ExtensionNumberResponse* response);

        void FillFileDescriptorResponse(
            const protobuf::FileDescriptor* file_desc,
            reflection::v1alpha::ServerReflectionResponse* response,
            std::unordered_set<std::string>* seen_files);

        void FillErrorResponse(const Status& status,
            reflection::v1alpha::ErrorResponse* error_response);

        const protobuf::DescriptorPool* descriptor_pool_;
        const std::vector<string>* services_;
    };

}  // namespace grpc
