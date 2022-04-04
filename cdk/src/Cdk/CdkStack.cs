using Amazon.CDK;
using Constructs;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ECS.Patterns;

namespace Cdk
{
    public class CdkStack : Stack
    {
        internal CdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var vpc = new Vpc(this, "DotNetGravitonVpc", new VpcProps
            {
                MaxAzs = 2
            });

            var cluster = new Cluster(this, "DotNetGravitonCluster", new ClusterProps
            {
                Vpc = vpc
            });

            FargateTaskDefinition fargateTaskDefinition = new FargateTaskDefinition(this, "TaskDef", new FargateTaskDefinitionProps
            {
                MemoryLimitMiB = 512,
                Cpu = 256,
                RuntimePlatform = new RuntimePlatform
                {
                    OperatingSystemFamily = OperatingSystemFamily.LINUX,
                    CpuArchitecture = CpuArchitecture.ARM64
                }
            });
            ContainerDefinition container = fargateTaskDefinition.AddContainer("WebContainer", new ContainerDefinitionOptions
            {
                // Use an image from DockerHub
                Image = ContainerImage.FromRegistry("arm64v8/busybox"),
            });

            container.AddPortMappings(new PortMapping
            {
                ContainerPort = 80,
                Protocol = Amazon.CDK.AWS.ECS.Protocol.TCP
            });


            ApplicationLoadBalancedFargateService loadBalancedFargateService = new ApplicationLoadBalancedFargateService(this, "Service", new ApplicationLoadBalancedFargateServiceProps
            {
                Cluster = cluster,
                MemoryLimitMiB = 1024,
                DesiredCount = 1,
                Cpu = 512,
                TaskDefinition = fargateTaskDefinition

            });
        }

    }
}
