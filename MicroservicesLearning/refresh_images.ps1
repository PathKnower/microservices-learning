#Rebuild and publish command service
.\MicroservicesLearning.CommandsService\docker_build.ps1
.\MicroservicesLearning.CommandsService\docker_push.ps1

#Rebuild and publish platform service
.\MicroservicesLearning.PlatformService\docker_build.ps1
.\MicroservicesLearning.PlatformService\docker_push.ps1